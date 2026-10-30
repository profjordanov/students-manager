using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
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

    [Fact]
    public async Task ConcurrentSubmissionClaimsAttemptBeforeCallingEvaluator()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();
        int attemptId;
        using (var context = NewContext())
        {
            var assigned = await CreateService(context, settings.Object, evaluator.Object).GetOrAssignAsync(data.UserId);
            attemptId = assigned.Value!.AttemptId;
        }

        var input = new RoboExaminationSubmissionInput
        {
            UserId = data.UserId,
            AttemptId = attemptId,
            Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "A clear answer" }]
        };
        var results = await Task.WhenAll(
            Task.Run(async () =>
            {
                using var context = NewContext();
                return await CreateService(context, settings.Object, evaluator.Object)
                    .SubmitAsync(input, CancellationToken.None);
            }),
            Task.Run(async () =>
            {
                using var context = NewContext();
                return await CreateService(context, settings.Object, evaluator.Object)
                    .SubmitAsync(input, CancellationToken.None);
            }));

        Assert.Single(results, result => result.Status == RoboExaminationServiceStatus.Success);
        Assert.Single(results, result => result.Status == RoboExaminationServiceStatus.AlreadySubmitted);
        evaluator.Verify(item => item.EvaluateAsync(
            It.IsAny<IReadOnlyList<RoboExaminationEvaluationItem>>(),
            It.IsAny<CancellationToken>()), Times.Once);
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
        int secondAttemptId;
        using (var context = NewContext())
        {
            var examination = new RoboExamination
            {
                Type = "Second",
                Variant = Guid.NewGuid().ToString("N"),
                StringifiedJsonContent = ExaminationContent
            };
            context.RoboExaminations.Add(examination);
            await context.SaveChangesAsync();
            var attempt = new RoboExaminationAttempt
            {
                UserId = data.UserId,
                RoboExaminationId = examination.Id,
                ExaminationType = "Second",
                AssignedAtUtc = DateTime.UtcNow,
                EvaluationStatus = RoboExaminationEvaluationStatus.Pending
            };
            context.RoboExaminationAttempts.Add(attempt);
            await context.SaveChangesAsync();
            secondAttemptId = attempt.Id;
        }

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

        using var submitContext = NewContext();
        var submitResult = await CreateService(
                submitContext,
                SettingsFor("Second").Object,
                EvaluatorReturningSuccess().Object)
            .SubmitAsync(
                new RoboExaminationSubmissionInput
                {
                    UserId = data.UserId,
                    AttemptId = secondAttemptId,
                    Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
                },
                CancellationToken.None);
        Assert.Equal(RoboExaminationServiceStatus.RoboQuizRequired, submitResult.Status);
    }

    [Fact]
    public async Task SoftDeletedUserCannotSubmit()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();
        int attemptId;
        using (var assignContext = NewContext())
        {
            var assignment = await CreateService(assignContext, settings.Object, evaluator.Object)
                .GetOrAssignAsync(data.UserId);
            attemptId = assignment.Value!.AttemptId;
        }
        using (var updateContext = NewContext())
        {
            var user = await updateContext.Users.SingleAsync(item => item.Id == data.UserId);
            user.IsDeleted = true;
            await updateContext.SaveChangesAsync();
        }

        using var submitContext = NewContext();
        var result = await CreateService(submitContext, settings.Object, evaluator.Object).SubmitAsync(
            new RoboExaminationSubmissionInput
            {
                UserId = data.UserId,
                AttemptId = attemptId,
                Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
            },
            CancellationToken.None);

        Assert.Equal(RoboExaminationServiceStatus.UserNotFound, result.Status);
    }

    [Fact]
    public async Task FailedEvaluationPersistsRawErrorAndCanBeReevaluated()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var evaluator = new Mock<IRoboExaminationEvaluator>();
        evaluator.SetupSequence(item => item.EvaluateAsync(
                It.IsAny<IReadOnlyList<RoboExaminationEvaluationItem>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new RoboExaminationEvaluationException("Invalid response.", "not-json"))
            .ReturnsAsync(new RoboExaminationEvaluation(4, "Recovered.", """{"grade":4,"overallFeedback":"Recovered."}"""));
        using var context = NewContext();
        var service = CreateService(context, SettingsFor("First").Object, evaluator.Object);
        var assignment = await service.GetOrAssignAsync(data.UserId);

        var submission = await service.SubmitAsync(
            new RoboExaminationSubmissionInput
            {
                UserId = data.UserId,
                AttemptId = assignment.Value!.AttemptId,
                Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
            },
            CancellationToken.None);

        Assert.Equal(RoboExaminationServiceStatus.Success, submission.Status);
        Assert.Equal("Failed", submission.Value!.EvaluationStatus);
        var attempt = await context.RoboExaminationAttempts
            .AsNoTracking()
            .SingleAsync(item => item.Id == assignment.Value.AttemptId);
        Assert.Equal("not-json", attempt.RawEvaluation);
        Assert.Equal("Invalid response.", attempt.EvaluationError);

        var reevaluation = await service.ReevaluateAsync(assignment.Value.AttemptId);

        Assert.Equal(RoboExaminationServiceStatus.Success, reevaluation.Status);
        Assert.Equal("Succeeded", reevaluation.Value!.EvaluationStatus);
        Assert.Equal(4, reevaluation.Value.Grade);
    }

    [Fact]
    public async Task RecentPendingEvaluationCannotBeClaimedButStalePendingCan()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluator = EvaluatorReturningSuccess();
        using var context = NewContext();
        var service = CreateService(context, settings.Object, evaluator.Object);
        var assignment = await service.GetOrAssignAsync(data.UserId);
        var submission = await service.SubmitAsync(
            new RoboExaminationSubmissionInput
            {
                UserId = data.UserId,
                AttemptId = assignment.Value!.AttemptId,
                Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
            },
            CancellationToken.None);
        Assert.Equal(RoboExaminationServiceStatus.Success, submission.Status);

        await context.RoboExaminationAttempts
            .Where(item => item.Id == assignment.Value.AttemptId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(item => item.EvaluationStatus, RoboExaminationEvaluationStatus.Pending)
                .SetProperty(item => item.EvaluationStartedAtUtc, DateTime.UtcNow));
        var recent = await service.ReevaluateAsync(assignment.Value.AttemptId);
        Assert.Equal(RoboExaminationServiceStatus.NotReevaluable, recent.Status);

        await context.RoboExaminationAttempts
            .Where(item => item.Id == assignment.Value.AttemptId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(item => item.EvaluationStartedAtUtc, DateTime.UtcNow.AddMinutes(-6)));
        var stale = await service.ReevaluateAsync(assignment.Value.AttemptId);

        Assert.Equal(RoboExaminationServiceStatus.Success, stale.Status);
        Assert.Equal("Succeeded", stale.Value!.EvaluationStatus);
    }

    [Fact]
    public async Task RequestCancellationAfterClaimDoesNotCancelEvaluation()
    {
        await EnsureDatabaseCreatedAsync();
        var data = await SeedUserAndExaminationAsync("First");
        var settings = SettingsFor("First");
        var evaluatorStarted = new TaskCompletionSource<CancellationToken>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        var releaseEvaluation = new TaskCompletionSource(
            TaskCreationOptions.RunContinuationsAsynchronously);
        var evaluator = new Mock<IRoboExaminationEvaluator>();
        evaluator.Setup(item => item.EvaluateAsync(
                It.IsAny<IReadOnlyList<RoboExaminationEvaluationItem>>(),
                It.IsAny<CancellationToken>()))
            .Returns((IReadOnlyList<RoboExaminationEvaluationItem> _, CancellationToken evaluationToken) =>
                EvaluateAfterReleaseAsync(evaluationToken));
        using var context = NewContext();
        var service = CreateService(context, settings.Object, evaluator.Object);
        var assignment = await service.GetOrAssignAsync(data.UserId);
        using var requestCancellation = new CancellationTokenSource();
        var submitTask = service.SubmitAsync(
            new RoboExaminationSubmissionInput
            {
                UserId = data.UserId,
                AttemptId = assignment.Value!.AttemptId,
                Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "Answer" }]
            },
            requestCancellation.Token);

        var evaluationToken = await evaluatorStarted.Task.WaitAsync(TimeSpan.FromSeconds(10));
        requestCancellation.Cancel();
        Assert.False(evaluationToken.IsCancellationRequested);
        releaseEvaluation.SetResult();

        var result = await submitTask;
        Assert.Equal(RoboExaminationServiceStatus.Success, result.Status);

        async Task<RoboExaminationEvaluation> EvaluateAfterReleaseAsync(CancellationToken evaluationToken)
        {
            evaluatorStarted.SetResult(evaluationToken);
            await releaseEvaluation.Task;
            return new RoboExaminationEvaluation(4, "Done.", """{"grade":4,"overallFeedback":"Done."}""");
        }
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

    private static Mock<IRoboExaminationEvaluator> EvaluatorReturningSuccess()
    {
        var evaluator = new Mock<IRoboExaminationEvaluator>();
        evaluator.Setup(service => service.EvaluateAsync(
                It.IsAny<IReadOnlyList<RoboExaminationEvaluationItem>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RoboExaminationEvaluation(5, "Good answer.", """{"grade":5,"overallFeedback":"Good answer."}"""));
        return evaluator;
    }

    private static ManagerDbContext NewContext() => new(DbContextProvider.GetDbContextOptions());

    private static RoboExaminationService CreateService(
        ManagerDbContext context,
        IExaminationSettingsService settings,
        IRoboExaminationEvaluator evaluator) =>
        new(context, settings, evaluator,
            NullLogger<RoboExaminationService>.Instance);
}
