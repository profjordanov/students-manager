using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.RoboQuizzes;
using StudentsManager.Mvc.Domain.Views.RoboQuizzes;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.CourseExaminations;

namespace StudentsManager.Mvc.Services.RoboQuizzes
{
    public class RoboQuizService(
        ManagerDbContext dbContext,
        IExaminationSettingsService examinationSettingsService) : IRoboQuizService
    {
        private static readonly JsonSerializerSettings CamelCaseSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public async Task<RoboQuizServiceResult<RoboQuizView>> GetNextAsync(Guid userId)
        {
            if (!await UserExistsAsync(userId))
                return new(RoboQuizServiceStatus.UserNotFound, null);

            var setting = await examinationSettingsService.GetFirstEnabledAsync();
            if (setting == null)
                return new(RoboQuizServiceStatus.NoActiveExamination, null);

            if (await HasResultAsync(userId, setting.Type))
                return new(RoboQuizServiceStatus.Exhausted, null);

            var attempt = await FindAttemptAsync(userId, setting.Type);
            if (attempt == null)
            {
                var quiz = await dbContext.RoboQuizzes
                    .AsNoTracking()
                    .Where(quiz => quiz.Type == setting.Type)
                    .OrderBy(_ => Guid.NewGuid())
                    .FirstOrDefaultAsync();
                if (quiz == null)
                    return new(RoboQuizServiceStatus.NoVariants, null);

                attempt = new RoboQuizAttempt
                {
                    UserId = userId,
                    RoboQuizId = quiz.Id,
                    ExaminationType = setting.Type,
                    AssignedAtUtc = DateTime.UtcNow
                };
                dbContext.RoboQuizAttempts.Add(attempt);
                try
                {
                    await dbContext.SaveChangesAsync();
                    attempt.Quiz = quiz;
                }
                catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
                {
                    dbContext.Entry(attempt).State = EntityState.Detached;
                    attempt = await FindAttemptAsync(userId, setting.Type);
                    if (attempt == null)
                        throw;
                }
            }

            var nextQuiz = attempt.Quiz;
            var questions = JsonConvert.DeserializeObject<List<RoboQuizQuestion>>(
                nextQuiz.StringifiedJsonContent);
            if (questions == null)
            {
                throw new JsonSerializationException(
                    $"RoboQuiz {nextQuiz.Id} content did not contain a question array.");
            }

            return new(
                RoboQuizServiceStatus.Success,
                new RoboQuizView
                {
                    Id = nextQuiz.Id,
                    Type = nextQuiz.Type,
                    Variant = nextQuiz.Variant,
                    Questions = questions
                });
        }

        public async Task<RoboQuizServiceResult<RoboQuizSummaryView>> CreateAsync(RoboQuizInput input)
        {
            var type = CourseExaminationConstants.NormalizeType(input.Type);
            if (type == null || !await dbContext.CourseExaminationSettings.AnyAsync(setting => setting.Type == type))
                return new(RoboQuizServiceStatus.InvalidType, null);

            var quiz = new RoboQuiz
            {
                Type = type,
                Variant = input.Variant.Trim(),
                StringifiedJsonContent = JsonConvert.SerializeObject(input.Questions, CamelCaseSettings)
            };

            dbContext.RoboQuizzes.Add(quiz);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                return new(RoboQuizServiceStatus.Duplicate, null);
            }

            return new(
                RoboQuizServiceStatus.Success,
                new RoboQuizSummaryView
                {
                    Id = quiz.Id,
                    Type = quiz.Type,
                    Variant = quiz.Variant
                });
        }

        public async Task<RoboQuizServiceResult<RoboQuizResultCreatedView>> SubmitResultAsync(
            RoboQuizResultInput input)
        {
            if (!await UserExistsAsync(input.UserId))
                return new(RoboQuizServiceStatus.UserNotFound, null);

            var quiz = await dbContext.RoboQuizzes
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == input.RoboQuizId);
            if (quiz == null)
                return new(RoboQuizServiceStatus.QuizNotFound, null);

            var setting = await examinationSettingsService.GetFirstEnabledAsync();
            if (setting == null || quiz.Type != setting.Type)
                return new(RoboQuizServiceStatus.Forbidden, null);

            if (await HasResultAsync(input.UserId, setting.Type))
                return new(RoboQuizServiceStatus.Duplicate, null);

            var attempt = await FindAttemptAsync(input.UserId, setting.Type);
            if (attempt == null || attempt.RoboQuizId != input.RoboQuizId)
                return new(RoboQuizServiceStatus.NotAssigned, null);

            var result = new RoboQuizUserResult
            {
                UserId = input.UserId,
                RoboQuizId = input.RoboQuizId,
                ScorePercentage = input.ScorePercentage,
                CompletedAtUtc = DateTime.UtcNow
            };
            dbContext.RoboQuizUserResults.Add(result);

            var isFirst = quiz.Type == CourseExaminationConstants.FirstType;
            var scorePoints = (decimal)(5 * input.ScorePercentage / 100.0);

            var userScore = await dbContext
                .UsersScore
                .FirstOrDefaultAsync(score => score.UserId == input.UserId);

            if (userScore == null)
            {
                userScore = new UserScore
                {
                    UserId = input.UserId
                };
                dbContext.UsersScore.Add(userScore);
            }

            if (isFirst)
            {
                userScore.FirstExaminationScore = scorePoints;
            }
            else
            {
                userScore.SecondExaminationScore = scorePoints;
            }

            // Save the result and scoreboard together; a duplicate must not overwrite the score.
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                if (!await HasResultAsync(input.UserId, setting.Type))
                    throw;
                return new(RoboQuizServiceStatus.Duplicate, null);
            }

            return new(
                RoboQuizServiceStatus.Success,
                new RoboQuizResultCreatedView
                {
                    Id = result.Id,
                    RoboQuizId = result.RoboQuizId,
                    ScorePercentage = result.ScorePercentage,
                    CompletedAtUtc = result.CompletedAtUtc
                });
        }

        public async Task<IReadOnlyList<RoboQuizResultView>> GetResultsAsync(Guid userId)
        {
            return await dbContext.RoboQuizUserResults
                .AsNoTracking()
                .Where(result => result.UserId == userId)
                .OrderByDescending(result => result.CompletedAtUtc)
                .Select(result => new RoboQuizResultView
                {
                    Id = result.Id,
                    Type = result.RoboQuiz.Type,
                    Variant = result.RoboQuiz.Variant,
                    ScorePercentage = result.ScorePercentage,
                    CompletedAtUtc = result.CompletedAtUtc
                })
                .ToListAsync();
        }

        private Task<bool> UserExistsAsync(Guid userId)
        {
            return dbContext.Users.AnyAsync(user => user.Id == userId && !user.IsDeleted);
        }

        private Task<bool> HasResultAsync(Guid userId, string type) =>
            dbContext.RoboQuizUserResults.AnyAsync(result => result.UserId == userId && result.RoboQuiz.Type == type);

        private Task<RoboQuizAttempt?> FindAttemptAsync(Guid userId, string type) =>
            dbContext.RoboQuizAttempts
                .AsNoTracking()
                .Include(attempt => attempt.Quiz)
                .SingleOrDefaultAsync(attempt => attempt.UserId == userId && attempt.ExaminationType == type);

    }
}
