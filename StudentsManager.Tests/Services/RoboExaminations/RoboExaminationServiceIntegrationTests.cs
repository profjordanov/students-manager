using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Domain.Views.RoboExaminations;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.RoboExaminations;
using StudentsManager.Tests.Providers;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

[Trait("Category", "Integration")]
public class RoboExaminationServiceIntegrationTests
{
    private static readonly object DatabaseInitializationLock = new();
    private const string ExaminationContent =
        """[{"id":"q1","text":"Explain this concept","type":"text","expectedAnswer":"A clear answer"}]""";

    [Fact]
    public async Task ConcurrentAssignmentReturnsOneStableAttempt()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();

        var results = await Task.WhenAll(
            Task.Run(async () =>
            {
                using var context = NewContext();
                return await CreateService(context, settings.Object, evaluator.Object).GetOrAssignAsync(data.UserId);
            }),
            Task.Run(async () =>
            {
                using var context = NewContext();
                return await CreateService(context, settings.Object, evaluator.Object).GetOrAssignAsync(data.UserId);
            }));

        Assert.All(results, result => Assert.Equal(RoboExaminationServiceStatus.Success, result.Status));
        Assert.Equal(results[0].Value!.AttemptId, results[1].Value!.AttemptId);
        using var verifyContext = NewContext();
        Assert.Equal(1, await verifyContext.RoboExaminationAttempts.CountAsync(attempt => attempt.UserId == data.UserId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task SubmissionPersistsZeroBasedGradeAndAnswerSnapshot(int grade)
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        using var context = NewContext();
        var evaluator = EvaluatorReturningSuccess(grade);
        var service = CreateService(context, SettingsFor("First").Object, evaluator.Object);
        var assignment = await service.GetOrAssignAsync(data.UserId);
        var question = Assert.Single(assignment.Value!.Questions);

        var submission = await service.SubmitAsync(Submission(data.UserId, assignment.Value.AttemptId));

        Assert.Equal(RoboExaminationServiceStatus.Success, submission.Status);
        Assert.Equal("Succeeded", submission.Value!.EvaluationStatus);
        Assert.Equal(grade, submission.Value.Grade);
        var attempt = await context.RoboExaminationAttempts.AsNoTracking()
            .SingleAsync(item => item.Id == assignment.Value.AttemptId);
        Assert.Equal(grade, attempt.Grade);
        Assert.Equal(submission.Value.SubmittedAtUtc, attempt.SubmittedAtUtc);
        var results = await service.GetResultsAsync(data.UserId);
        var result = Assert.Single(results.Value!);
        Assert.Equal(grade, result.Grade);
        Assert.Equal(submission.Value.Feedback, result.Feedback);
        var savedAnswer = Assert.Single(result.Answers);
        Assert.Equal(question.Id, savedAnswer.QuestionId);
        Assert.Equal(question.Text, savedAnswer.QuestionText);
        Assert.Equal("Answer", savedAnswer.Answer);
        evaluator.Verify(item => item.EvaluateAsync(
            It.Is<IReadOnlyList<RoboExaminationAnswerView>>(answers =>
                answers.Count == 1 && answers[0].QuestionText == question.Text && answers[0].Answer == "Answer")),
            Times.Once);
    }

    [Fact]
    public async Task SubmissionSnapshotContainsOnlyExaminationQuestions()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        using var context = NewContext();
        var evaluator = EvaluatorReturningSuccess();
        var service = CreateService(context, SettingsFor("First").Object, evaluator.Object);
        var assignment = await service.GetOrAssignAsync(data.UserId);

        var submission = await service.SubmitAsync(new RoboExaminationSubmissionInput
        {
            UserId = data.UserId,
            AttemptId = assignment.Value!.AttemptId,
            Answers = [new RoboExaminationAnswerInput { QuestionId = "unknown", Answer = "Ignored" }]
        });

        Assert.Equal(RoboExaminationServiceStatus.Success, submission.Status);
        evaluator.Verify(item => item.EvaluateAsync(
            It.Is<IReadOnlyList<RoboExaminationAnswerView>>(answers =>
                answers.Count == 1 && answers[0].QuestionId == "q1" && answers[0].Answer == string.Empty)),
            Times.Once);
    }

    [Fact]
    public async Task MatchingQuizTypeUnlocksExaminationRegardlessOfScore()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First", scorePercentage: 0);
        using var context = NewContext();
        var result = await CreateService(
                context,
                SettingsFor("First").Object,
                EvaluatorReturningSuccess().Object)
            .GetOrAssignAsync(data.UserId);

        Assert.Equal(RoboExaminationServiceStatus.Success, result.Status);
    }

    [Fact]
    public async Task NoActiveSettingAndMismatchedQuizTypeAreRejected()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");

        using var noSettingContext = NewContext();
        var noSettingResult = await CreateService(
                noSettingContext,
                SettingsWithoutActiveType().Object,
                EvaluatorReturningSuccess().Object)
            .GetOrAssignAsync(data.UserId);

        using var mismatchedContext = NewContext();
        var mismatchedResult = await CreateService(
                mismatchedContext,
                SettingsFor("Second").Object,
                EvaluatorReturningSuccess().Object)
            .GetOrAssignAsync(data.UserId);

        Assert.Equal(RoboExaminationServiceStatus.NoActiveExamination, noSettingResult.Status);
        Assert.Equal(RoboExaminationServiceStatus.RoboQuizRequired, mismatchedResult.Status);
    }

    [Fact]
    public async Task AttemptCanBeSubmittedOnlyOnceByItsOwner()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();
        using var context = NewContext();
        var service = CreateService(context, settings.Object, evaluator.Object);
        var attemptId = (await service.GetOrAssignAsync(data.UserId)).Value!.AttemptId;

        var otherUserSubmission = await service.SubmitAsync(Submission(Guid.NewGuid(), attemptId));
        var ownerSubmission = await service.SubmitAsync(Submission(data.UserId, attemptId));
        using var resubmitContext = NewContext();
        var resubmission = await CreateService(resubmitContext, settings.Object, evaluator.Object)
            .SubmitAsync(Submission(data.UserId, attemptId));

        Assert.Equal(RoboExaminationServiceStatus.AttemptNotFound, otherUserSubmission.Status);
        Assert.Equal(RoboExaminationServiceStatus.Success, ownerSubmission.Status);
        Assert.Equal(RoboExaminationServiceStatus.AlreadySubmitted, resubmission.Status);
        evaluator.Verify(item => item.EvaluateAsync(It.IsAny<IReadOnlyList<RoboExaminationAnswerView>>()), Times.Once);
    }

    [Fact]
    public async Task FailedEvaluationCanBeReevaluatedFromSavedAnswers()
    {
        await EnsureDatabaseCreatedAsync();
        var type = Guid.NewGuid().ToString("N");
        var data = await SeedUserAndExaminationAsync(type);
        var settings = SettingsFor(type);
        var evaluator = new Mock<IRoboExaminationEvaluator>();
        evaluator.SetupSequence(item => item.EvaluateAsync(It.IsAny<IReadOnlyList<RoboExaminationAnswerView>>()))
            .ThrowsAsync(new HttpRequestException("Azure OpenAI is unavailable."))
            .ReturnsAsync(new RoboExaminationEvaluation(1, "Recovered."));
        int attemptId;
        DateTime? submittedAtUtc;
        using (var context = NewContext())
        {
            var service = CreateService(context, settings.Object, evaluator.Object);
            attemptId = (await service.GetOrAssignAsync(data.UserId)).Value!.AttemptId;

            var submission = await service.SubmitAsync(Submission(data.UserId, attemptId));

            Assert.Equal(RoboExaminationServiceStatus.Success, submission.Status);
            Assert.Equal("Failed", submission.Value!.EvaluationStatus);
            Assert.Null(submission.Value.Grade);
            Assert.Null(submission.Value.Feedback);
            submittedAtUtc = submission.Value.SubmittedAtUtc;
            await context.RoboExaminations
                .Where(item => item.Type == type)
                .ExecuteUpdateAsync(setters => setters.SetProperty(item => item.StringifiedJsonContent, "[]"));
        }

        using var reevaluateContext = NewContext();
        var reevaluation = await CreateService(reevaluateContext, settings.Object, evaluator.Object)
            .ReevaluateAsync(attemptId);

        Assert.Equal(RoboExaminationServiceStatus.Success, reevaluation.Status);
        Assert.Equal("Succeeded", reevaluation.Value!.EvaluationStatus);
        Assert.Equal(1, reevaluation.Value.Grade);
        Assert.Equal("Recovered.", reevaluation.Value.Feedback);
        Assert.Equal(submittedAtUtc, reevaluation.Value.SubmittedAtUtc);
        evaluator.Verify(item => item.EvaluateAsync(
            It.Is<IReadOnlyList<RoboExaminationAnswerView>>(answers =>
                answers.Count == 1 && answers[0].QuestionText == "Explain this concept" && answers[0].Answer == "Answer")),
            Times.Exactly(2));
    }

    [Fact]
    public async Task OnlySubmittedAttemptsWithoutSuccessfulEvaluationCanBeReevaluated()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();
        int attemptId;
        using (var context = NewContext())
        {
            var service = CreateService(context, settings.Object, evaluator.Object);
            attemptId = (await service.GetOrAssignAsync(data.UserId)).Value!.AttemptId;

            Assert.Equal(RoboExaminationServiceStatus.AttemptNotFound, (await service.ReevaluateAsync(-1)).Status);
            Assert.Equal(RoboExaminationServiceStatus.NotReevaluable, (await service.ReevaluateAsync(attemptId)).Status);
            await service.SubmitAsync(Submission(data.UserId, attemptId));
            Assert.Equal(RoboExaminationServiceStatus.NotReevaluable, (await service.ReevaluateAsync(attemptId)).Status);

            await context.RoboExaminationAttempts
                .Where(item => item.Id == attemptId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(item => item.EvaluationStatus, RoboExaminationEvaluationStatus.Pending));
        }

        using var pendingContext = NewContext();
        var reevaluation = await CreateService(pendingContext, settings.Object, evaluator.Object)
            .ReevaluateAsync(attemptId);

        Assert.Equal(RoboExaminationServiceStatus.Success, reevaluation.Status);
        Assert.Equal("Succeeded", reevaluation.Value!.EvaluationStatus);
    }

    private static async Task EnsureDatabaseCreatedAsync()
    {
        lock (DatabaseInitializationLock)
        {
            using var context = NewContext();
            context.Database.EnsureCreated();
        }
        await Task.CompletedTask;
    }

    private static async Task<(Guid UserId, int QuizId)> SeedUserAndExaminationAsync(
        string type,
        int scorePercentage = 50)
    {
        var userId = Guid.NewGuid();
        var variant = Guid.NewGuid().ToString("N");
        using var context = NewContext();
        var user = new User
        {
            Id = userId,
            UserName = $"robo-exam-{userId:N}",
            NormalizedUserName = $"ROBO-EXAM-{userId:N}",
            Email = $"{userId:N}@example.test",
            NormalizedEmail = $"{userId:N}@EXAMPLE.TEST",
            FullName = "RoboExamination test student",
            FacultyNumber = userId.ToString("N"),
            CreatedOn = DateTime.UtcNow
        };
        var quiz = new RoboQuiz
        {
            Type = type,
            Variant = variant,
            StringifiedJsonContent = "[]"
        };
        var examination = new RoboExamination
        {
            Type = type,
            Variant = variant,
            StringifiedJsonContent = ExaminationContent
        };
        context.Users.Add(user);
        context.RoboQuizzes.Add(quiz);
        context.RoboExaminations.Add(examination);
        await context.SaveChangesAsync();
        context.RoboQuizUserResults.Add(new RoboQuizUserResult
        {
            UserId = userId,
            RoboQuizId = quiz.Id,
            ScorePercentage = scorePercentage,
            CompletedAtUtc = DateTime.UtcNow
        });
        await context.SaveChangesAsync();
        return (userId, quiz.Id);
    }

    private static Mock<IExaminationSettingsService> SettingsFor(string type)
    {
        var settings = new Mock<IExaminationSettingsService>();
        settings.Setup(service => service.GetFirstEnabledAsync())
            .ReturnsAsync(new CourseExaminationSetting { Id = 1, Type = type, Enabled = true });
        return settings;
    }

    private static Mock<IExaminationSettingsService> SettingsWithoutActiveType()
    {
        var settings = new Mock<IExaminationSettingsService>();
        settings.Setup(service => service.GetFirstEnabledAsync()).ReturnsAsync((CourseExaminationSetting)null);
        return settings;
    }

    private static Mock<IRoboExaminationEvaluator> EvaluatorReturningSuccess(int grade = 2)
    {
        var evaluator = new Mock<IRoboExaminationEvaluator>();
        evaluator.Setup(service => service.EvaluateAsync(It.IsAny<IReadOnlyList<RoboExaminationAnswerView>>()))
            .ReturnsAsync(new RoboExaminationEvaluation(grade, "Good answer."));
        return evaluator;
    }

    private static RoboExaminationSubmissionInput Submission(Guid userId, int attemptId) => new()
    {
        UserId = userId,
        AttemptId = attemptId,
        Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
    };

    private static ManagerDbContext NewContext() => new(DbContextProvider.GetDbContextOptions());

    private static RoboExaminationService CreateService(
        ManagerDbContext context,
        IExaminationSettingsService settings,
        IRoboExaminationEvaluator evaluator) =>
        new(context, settings, evaluator,
            NullLogger<RoboExaminationService>.Instance);
}
