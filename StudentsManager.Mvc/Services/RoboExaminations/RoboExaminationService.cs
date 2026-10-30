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
            RoboExaminationSubmissionInput input,
            CancellationToken requestAborted)
        {
            if (!await UserExistsAsync(input.UserId, requestAborted))
                return new(RoboExaminationServiceStatus.UserNotFound, null);

            var attempt = await dbContext.RoboExaminationAttempts
                .AsNoTracking()
                .Include(item => item.Examination)
                .FirstOrDefaultAsync(item => item.Id == input.AttemptId, requestAborted);
            if (attempt == null || attempt.UserId != input.UserId)
                return new(RoboExaminationServiceStatus.AttemptNotFound, null);
            if (attempt.SubmittedAtUtc != null)
                return new(RoboExaminationServiceStatus.AlreadySubmitted, null);

            var setting = await examinationSettingsService.GetFirstEnabledAsync();
            if (setting == null || setting.Type != attempt.ExaminationType)
                return new(RoboExaminationServiceStatus.NoActiveExamination, null);

            if (!await dbContext.RoboQuizUserResults.AnyAsync(
                    result => result.UserId == input.UserId && result.RoboQuiz.Type == attempt.ExaminationType,
                    requestAborted))
                return new(RoboExaminationServiceStatus.RoboQuizRequired, null);

            var questions = DeserializeQuestions(attempt.Examination.StringifiedJsonContent);
            var validationMessage = ValidateAnswers(questions, input.Answers);
            if (validationMessage != null)
                return new(RoboExaminationServiceStatus.InvalidAnswers, null, validationMessage);

            var answersById = input.Answers.ToDictionary(answer => answer.QuestionId, StringComparer.Ordinal);
            var savedAnswers = questions.Select(question => new StoredAnswer(
                question.Id,
                question.Text,
                answersById[question.Id].Answer.Trim())).ToList();
            var evaluationItems = BuildEvaluationItems(questions, savedAnswers);

            var claimStartedAtUtc = DateTime.UtcNow;
            var submittedAtUtc = claimStartedAtUtc;
            var answersJson = JsonConvert.SerializeObject(savedAnswers, CamelCaseSettings);
            var claimedRows = await dbContext.RoboExaminationAttempts
                .Where(item => item.Id == attempt.Id && item.SubmittedAtUtc == null)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(item => item.SubmittedAtUtc, submittedAtUtc)
                    .SetProperty(item => item.AnswersJson, answersJson)
                    .SetProperty(item => item.EvaluationStatus, RoboExaminationEvaluationStatus.Pending)
                    .SetProperty(item => item.EvaluationStartedAtUtc, claimStartedAtUtc)
                    .SetProperty(item => item.EvaluationError, (string?)null)
                    .SetProperty(item => item.Grade, (int?)null)
                    .SetProperty(item => item.Feedback, (string?)null)
                    .SetProperty(item => item.RawEvaluation, (string?)null));
            if (claimedRows == 0)
                return new(RoboExaminationServiceStatus.AlreadySubmitted, null);

            var evaluationResult = await EvaluateAndStoreAsync(attempt.Id, claimStartedAtUtc, evaluationItems);
            if (evaluationResult == RoboExaminationServiceStatus.EvaluationSuperseded)
                return new(RoboExaminationServiceStatus.EvaluationSuperseded, null);

            var submission = await GetSubmissionViewAsync(attempt.Id);
            return new(RoboExaminationServiceStatus.Success, submission);
        }

        public async Task<RoboExaminationServiceResult<RoboExaminationSubmissionView>> ReevaluateAsync(int attemptId)
        {
            var claimStartedAtUtc = DateTime.UtcNow;
            var staleBeforeUtc = claimStartedAtUtc.AddMinutes(-5);
            var claimedRows = await dbContext.RoboExaminationAttempts
                .Where(attempt => attempt.Id == attemptId
                    && attempt.SubmittedAtUtc != null
                    && (attempt.EvaluationStatus == RoboExaminationEvaluationStatus.Failed
                        || (attempt.EvaluationStatus == RoboExaminationEvaluationStatus.Pending
                            && attempt.EvaluationStartedAtUtc < staleBeforeUtc)))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(attempt => attempt.EvaluationStatus, RoboExaminationEvaluationStatus.Pending)
                    .SetProperty(attempt => attempt.EvaluationStartedAtUtc, claimStartedAtUtc)
                    .SetProperty(attempt => attempt.EvaluationError, (string?)null)
                    .SetProperty(attempt => attempt.Grade, (int?)null)
                    .SetProperty(attempt => attempt.Feedback, (string?)null)
                    .SetProperty(attempt => attempt.RawEvaluation, (string?)null));

            if (claimedRows == 0)
            {
                var exists = await dbContext.RoboExaminationAttempts.AnyAsync(attempt => attempt.Id == attemptId);
                return new(
                    exists ? RoboExaminationServiceStatus.NotReevaluable : RoboExaminationServiceStatus.AttemptNotFound,
                    null);
            }

            var attempt = await dbContext.RoboExaminationAttempts
                .AsNoTracking()
                .Include(item => item.Examination)
                .SingleAsync(item => item.Id == attemptId);
            var questions = DeserializeQuestions(attempt.Examination.StringifiedJsonContent);
            var savedAnswers = JsonConvert.DeserializeObject<List<StoredAnswer>>(attempt.AnswersJson ?? "[]") ?? [];
            var evaluationItems = BuildEvaluationItems(questions, savedAnswers);
            var result = await EvaluateAndStoreAsync(attemptId, claimStartedAtUtc, evaluationItems);
            if (result == RoboExaminationServiceStatus.EvaluationSuperseded)
                return new(RoboExaminationServiceStatus.EvaluationSuperseded, null);

            return new(
                RoboExaminationServiceStatus.Success,
                await GetSubmissionViewAsync(attemptId));
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
                Answers = JsonConvert.DeserializeObject<List<StoredAnswer>>(attempt.AnswersJson ?? "[]")?
                    .Select(answer => new RoboExaminationAnswerView
                    {
                        QuestionId = answer.QuestionId,
                        QuestionText = answer.QuestionText,
                        Answer = answer.Answer
                    }).ToList() ?? []
            }).ToList();

            return new(RoboExaminationServiceStatus.Success, results);
        }

        private async Task<RoboExaminationServiceStatus> EvaluateAndStoreAsync(
            int attemptId,
            DateTime claimStartedAtUtc,
            IReadOnlyList<RoboExaminationEvaluationItem> items)
        {
            RoboExaminationEvaluation? evaluation = null;
            string? evaluationError = null;
            string? rawEvaluation = null;
            var status = RoboExaminationEvaluationStatus.Succeeded;

            using (var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
            {
                try
                {
                    evaluation = await evaluator.EvaluateAsync(items, timeout.Token);
                    rawEvaluation = evaluation.Raw;
                }
                catch (RoboExaminationEvaluationException exception)
                {
                    status = RoboExaminationEvaluationStatus.Failed;
                    evaluationError = Truncate(exception.Message, 2000);
                    rawEvaluation = exception.Raw;
                    logger.LogError(exception, "RoboExamination attempt {AttemptId} evaluation failed.", attemptId);
                }
                catch (OperationCanceledException exception) when (timeout.IsCancellationRequested)
                {
                    status = RoboExaminationEvaluationStatus.Failed;
                    evaluationError = "Evaluation timed out after 60 seconds.";
                    logger.LogError(exception, "RoboExamination attempt {AttemptId} evaluation timed out.", attemptId);
                }
            }

            using var persistenceTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            int updatedRows;
            try
            {
                updatedRows = await dbContext.RoboExaminationAttempts
                    .Where(attempt => attempt.Id == attemptId
                        && attempt.EvaluationStartedAtUtc == claimStartedAtUtc
                        && attempt.EvaluationStatus == RoboExaminationEvaluationStatus.Pending)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(attempt => attempt.EvaluationStatus, status)
                        .SetProperty(attempt => attempt.Grade, evaluation == null ? null : evaluation.Grade)
                        .SetProperty(attempt => attempt.Feedback, evaluation == null ? null : evaluation.Feedback)
                        .SetProperty(attempt => attempt.RawEvaluation, rawEvaluation)
                        .SetProperty(attempt => attempt.EvaluationError, evaluationError),
                        persistenceTimeout.Token);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Could not persist evaluation state for RoboExamination attempt {AttemptId}.", attemptId);
                throw;
            }

            if (updatedRows == 0)
            {
                logger.LogWarning("RoboExamination attempt {AttemptId} evaluation was superseded.", attemptId);
                return RoboExaminationServiceStatus.EvaluationSuperseded;
            }

            return RoboExaminationServiceStatus.Success;
        }

        private async Task<RoboExaminationSubmissionView> GetSubmissionViewAsync(int attemptId)
        {
            return await dbContext.RoboExaminationAttempts
                .AsNoTracking()
                .Where(attempt => attempt.Id == attemptId)
                .Select(attempt => new RoboExaminationSubmissionView
                {
                    AttemptId = attempt.Id,
                    SubmittedAtUtc = attempt.SubmittedAtUtc,
                    EvaluationStatus = attempt.EvaluationStatus.ToString(),
                    Grade = attempt.Grade,
                    Feedback = attempt.Feedback
                })
                .SingleAsync();
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

        private static string? ValidateAnswers(
            IReadOnlyList<RoboExaminationQuestion> questions,
            IReadOnlyList<RoboExaminationAnswerInput> answers)
        {
            if (answers.Any(answer => answer == null || string.IsNullOrWhiteSpace(answer.QuestionId)))
                return "Every answer must be an object with a questionId.";
            if (answers.GroupBy(answer => answer.QuestionId, StringComparer.Ordinal).Any(group => group.Count() > 1))
                return "Each question must be answered exactly once; duplicate questionId values were provided.";

            var questionIds = questions.Select(question => question.Id).ToHashSet(StringComparer.Ordinal);
            var answerIds = answers.Select(answer => answer.QuestionId).ToHashSet(StringComparer.Ordinal);
            if (!questionIds.SetEquals(answerIds))
                return "Answers must contain every examination question exactly once and no other question IDs.";

            foreach (var question in questions)
            {
                var answer = answers.Single(item => item.QuestionId == question.Id).Answer;
                if (string.IsNullOrWhiteSpace(answer))
                    return $"Answer for question {question.Id} cannot be empty.";
                if (answer.Length > 2000)
                    return $"Answer for question {question.Id} cannot exceed 2000 characters.";
            }

            return null;
        }

        private static IReadOnlyList<RoboExaminationEvaluationItem> BuildEvaluationItems(
            IReadOnlyList<RoboExaminationQuestion> questions,
            IReadOnlyList<StoredAnswer> answers)
        {
            var answerById = answers.ToDictionary(answer => answer.QuestionId, StringComparer.Ordinal);
            return questions.Select(question => new RoboExaminationEvaluationItem(
                question.Text,
                answerById[question.Id].Answer)).ToList();
        }

        private static List<RoboExaminationQuestion> DeserializeQuestions(string content)
        {
            return JsonConvert.DeserializeObject<List<RoboExaminationQuestion>>(content)
                ?? throw new JsonSerializationException("RoboExamination content did not contain a question array.");
        }

        private Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return dbContext.Users.AnyAsync(user => user.Id == userId && !user.IsDeleted, cancellationToken);
        }

        private static string Truncate(string value, int maximumLength) =>
            value.Length <= maximumLength ? value : value[..maximumLength];

        private sealed record StoredAnswer(string QuestionId, string QuestionText, string Answer);
    }
}
