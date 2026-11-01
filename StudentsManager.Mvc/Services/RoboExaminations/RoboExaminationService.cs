using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Domain.Views.RoboExaminations;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.CourseExaminations;

namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public sealed class RoboExaminationService(
        ManagerDbContext dbContext,
        IExaminationSettingsService examinationSettingsService,
        IRoboExaminationEvaluator evaluator,
        ILogger<RoboExaminationService> logger) : IRoboExaminationService
    {
        private static readonly JsonSerializerSettings CamelCaseSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public async Task<RoboExaminationServiceResult<RoboExaminationView>> GetOrAssignAsync(Guid userId)
        {
            if (!await UserExistsAsync(userId))
                return new(RoboExaminationServiceStatus.UserNotFound, null);

            var setting = await examinationSettingsService.GetFirstEnabledAsync();
            if (setting == null)
                return new(RoboExaminationServiceStatus.NoActiveExamination, null);

            if (!await dbContext.RoboQuizUserResults
                    .AnyAsync(result => result.UserId == userId && result.RoboQuiz.Type == setting.Type))
                return new(RoboExaminationServiceStatus.RoboQuizRequired, null);

            var existingAttempt = await FindAttemptAsync(userId, setting.Type);
            if (existingAttempt != null)
                return ExistingAttemptResult(existingAttempt);

            var examination = await dbContext.RoboExaminations
                .AsNoTracking()
                .Where(item => item.Type == setting.Type)
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefaultAsync();
            if (examination == null)
                return new(RoboExaminationServiceStatus.NoVariants, null);

            var attempt = new RoboExaminationAttempt
            {
                UserId = userId,
                RoboExaminationId = examination.Id,
                ExaminationType = setting.Type,
                AssignedAtUtc = DateTime.UtcNow,
                EvaluationStatus = RoboExaminationEvaluationStatus.Pending
            };
            dbContext.RoboExaminationAttempts.Add(attempt);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                dbContext.Entry(attempt).State = EntityState.Detached;
                existingAttempt = await FindAttemptAsync(userId, setting.Type);
                if (existingAttempt == null)
                    throw;
                return ExistingAttemptResult(existingAttempt);
            }

            return new(
                RoboExaminationServiceStatus.Success,
                ToExaminationView(attempt, examination));
        }

        public async Task<RoboExaminationServiceResult<RoboExaminationSummaryView>> CreateAsync(
            RoboExaminationInput input)
        {
            var type = CourseExaminationConstants.NormalizeType(input.Type);
            if (type == null || !await dbContext.CourseExaminationSettings.AnyAsync(setting => setting.Type == type))
                return new(RoboExaminationServiceStatus.InvalidType, null);

            var examination = new RoboExamination
            {
                Type = type,
                Variant = input.Variant.Trim(),
                StringifiedJsonContent = JsonConvert.SerializeObject(input.Questions, CamelCaseSettings)
            };
            dbContext.RoboExaminations.Add(examination);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                return new(RoboExaminationServiceStatus.Duplicate, null);
            }

            return new(
                RoboExaminationServiceStatus.Success,
                new RoboExaminationSummaryView
                {
                    Id = examination.Id,
                    Type = examination.Type,
                    Variant = examination.Variant
                });
        }

        public async Task<RoboExaminationServiceResult<RoboExaminationSubmissionView>> SubmitAsync(
            RoboExaminationSubmissionInput input)
        {
            var attempt = await dbContext.RoboExaminationAttempts
                .Include(item => item.Examination)
                .FirstOrDefaultAsync(item => item.Id == input.AttemptId && item.UserId == input.UserId);
            if (attempt == null)
                return new(RoboExaminationServiceStatus.AttemptNotFound, null);
            if (attempt.SubmittedAtUtc != null)
                return new(RoboExaminationServiceStatus.AlreadySubmitted, null);

            var answers = DeserializeQuestions(attempt.Examination.StringifiedJsonContent)
                .Select(question => new RoboExaminationAnswerView
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    Answer = input.Answers.FirstOrDefault(answer => answer.QuestionId == question.Id)?.Answer ?? string.Empty
                })
                .ToList();

            attempt.SubmittedAtUtc = DateTime.UtcNow;
            attempt.AnswersJson = JsonConvert.SerializeObject(answers, CamelCaseSettings);
            await dbContext.SaveChangesAsync();

            await EvaluateAsync(attempt, answers);
            return new(RoboExaminationServiceStatus.Success, ToSubmissionView(attempt));
        }

        public async Task<RoboExaminationServiceResult<RoboExaminationSubmissionView>> ReevaluateAsync(int attemptId)
        {
            var attempt = await dbContext.RoboExaminationAttempts.FindAsync(attemptId);
            if (attempt == null)
                return new(RoboExaminationServiceStatus.AttemptNotFound, null);
            if (attempt.SubmittedAtUtc == null || attempt.EvaluationStatus == RoboExaminationEvaluationStatus.Succeeded)
                return new(RoboExaminationServiceStatus.NotReevaluable, null);

            await EvaluateAsync(attempt, DeserializeAnswers(attempt.AnswersJson));
            return new(RoboExaminationServiceStatus.Success, ToSubmissionView(attempt));
        }

        public async Task<RoboExaminationServiceResult<IReadOnlyList<RoboExaminationResultView>>> GetResultsAsync(
            Guid userId)
        {
            if (!await UserExistsAsync(userId))
                return new(RoboExaminationServiceStatus.UserNotFound, null);

            var attempts = await dbContext.RoboExaminationAttempts
                .AsNoTracking()
                .Where(attempt => attempt.UserId == userId && attempt.SubmittedAtUtc != null)
                .OrderByDescending(attempt => attempt.SubmittedAtUtc)
                .Select(attempt => new
                {
                    attempt.Id,
                    attempt.Examination.Type,
                    attempt.Examination.Variant,
                    attempt.SubmittedAtUtc,
                    attempt.EvaluationStatus,
                    attempt.Grade,
                    attempt.Feedback,
                    attempt.AnswersJson
                })
                .ToListAsync();

            var results = attempts.Select(attempt => new RoboExaminationResultView
            {
                AttemptId = attempt.Id,
                Type = attempt.Type,
                Variant = attempt.Variant,
                SubmittedAtUtc = attempt.SubmittedAtUtc,
                EvaluationStatus = attempt.EvaluationStatus.ToString(),
                Grade = attempt.Grade,
                Feedback = attempt.Feedback,
                Answers = DeserializeAnswers(attempt.AnswersJson)
            }).ToList();

            return new(RoboExaminationServiceStatus.Success, results);
        }

        private async Task EvaluateAsync(RoboExaminationAttempt attempt, IReadOnlyList<RoboExaminationAnswerView> answers)
        {
            try
            {
                var evaluation = await evaluator.EvaluateAsync(answers);
                attempt.Grade = evaluation.Grade;
                attempt.Feedback = evaluation.Feedback;
                attempt.EvaluationStatus = RoboExaminationEvaluationStatus.Succeeded;
            }
            catch (Exception exception)
            {
                // The answers are already saved, so a failed evaluation is recorded and can be retried later.
                logger.LogError(exception, "RoboExamination attempt {AttemptId} evaluation failed.", attempt.Id);
                attempt.EvaluationStatus = RoboExaminationEvaluationStatus.Failed;
            }

            var userScore = await dbContext
                .UsersScore
                .FirstOrDefaultAsync(us => us.UserId == attempt.UserId);

            if (userScore != null)
            {
                var isFirst = attempt.Examination.Type == CourseExaminationConstants.FirstType;

                if (isFirst)
                {
                    userScore.FirstExaminationScore += (decimal)attempt.Grade!;
                }
                else
                {
                    userScore.SecondExaminationScore += (decimal)attempt.Grade!;
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private static RoboExaminationSubmissionView ToSubmissionView(RoboExaminationAttempt attempt)
        {
            return new RoboExaminationSubmissionView
            {
                AttemptId = attempt.Id,
                SubmittedAtUtc = attempt.SubmittedAtUtc,
                EvaluationStatus = attempt.EvaluationStatus.ToString(),
                Grade = attempt.Grade,
                Feedback = attempt.Feedback
            };
        }

        private async Task<RoboExaminationAttempt?> FindAttemptAsync(Guid userId, string type)
        {
            return await dbContext.RoboExaminationAttempts
                .AsNoTracking()
                .Include(attempt => attempt.Examination)
                .FirstOrDefaultAsync(attempt => attempt.UserId == userId && attempt.ExaminationType == type);
        }

        private static RoboExaminationServiceResult<RoboExaminationView> ExistingAttemptResult(
            RoboExaminationAttempt attempt)
        {
            return attempt.SubmittedAtUtc != null
                ? new(RoboExaminationServiceStatus.AlreadySubmitted, null)
                : new(RoboExaminationServiceStatus.Success, ToExaminationView(attempt, attempt.Examination));
        }

        private static RoboExaminationView ToExaminationView(
            RoboExaminationAttempt attempt,
            RoboExamination examination)
        {
            var questions = DeserializeQuestions(examination.StringifiedJsonContent);
            return new RoboExaminationView
            {
                AttemptId = attempt.Id,
                RoboExaminationId = examination.Id,
                Type = examination.Type,
                Variant = examination.Variant,
                Questions = questions.Select(question => new RoboExaminationQuestionView
                {
                    Id = question.Id,
                    Text = question.Text
                }).ToList()
            };
        }

        private static List<RoboExaminationAnswerView> DeserializeAnswers(string? content)
        {
            return JsonConvert.DeserializeObject<List<RoboExaminationAnswerView>>(content ?? "[]") ?? [];
        }

        private static List<RoboExaminationQuestion> DeserializeQuestions(string content)
        {
            return JsonConvert.DeserializeObject<List<RoboExaminationQuestion>>(content) ?? [];
        }

        private Task<bool> UserExistsAsync(Guid userId)
        {
            return dbContext.Users.AnyAsync(user => user.Id == userId && !user.IsDeleted);
        }
    }
}
