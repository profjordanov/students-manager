using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.PersonalityTests;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.PersonalityTests
{
    public class PersonalityTestsService : IPersonalityTestsService
    {
        private readonly ManagerDbContext _dbContext;

        public PersonalityTestsService(ManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> HasCompletedAsync(Guid userId)
        {
            return _dbContext.PersonalityTestResults.AnyAsync(result => result.UserId == userId);
        }

        public async Task<bool> RecordChoiceAsync(Guid userId, int questionNumber, int answerNumber)
        {
            var question = PersonalityTestConstants.Questions.SingleOrDefault(q => q.Number == questionNumber);
            if (question == null || answerNumber < 1 || answerNumber > question.Answers.Count)
            {
                return false;
            }

            var choice = new PersonalityTestChoice
            {
                UserId = userId,
                QuestionNumber = questionNumber,
                AnswerNumber = answerNumber,
                Trait = question.Answers[answerNumber - 1].Trait,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            await _dbContext.PersonalityTestChoices.AddAsync(choice);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteAsync(Guid userId)
        {
            var choices = await _dbContext.PersonalityTestChoices
                .AsNoTracking()
                .Where(choice => choice.UserId == userId)
                .Select(choice => new { choice.Id, choice.QuestionNumber, choice.Trait })
                .ToListAsync();

            // Every choice is kept, so a question answered more than once counts with its latest choice.
            var traits = choices
                .GroupBy(choice => choice.QuestionNumber)
                .Select(group => group.OrderByDescending(choice => choice.Id).First().Trait)
                .ToList();

            if (traits.Count < PersonalityTestConstants.Questions.Count)
            {
                return false;
            }

            int Score(PersonalityTrait trait) => traits.Count(t => t == trait);
            decimal Percentage(PersonalityTrait trait) => Math.Round(Score(trait) * 100m / MaxScore(trait), 2);

            var result = new PersonalityTestResult
            {
                UserId = userId,
                ActionScore = Score(PersonalityTrait.Action),
                ProcessScore = Score(PersonalityTrait.Process),
                PeopleScore = Score(PersonalityTrait.People),
                IdeaScore = Score(PersonalityTrait.Idea),
                ActionPercentage = Percentage(PersonalityTrait.Action),
                ProcessPercentage = Percentage(PersonalityTrait.Process),
                PeoplePercentage = Percentage(PersonalityTrait.People),
                IdeaPercentage = Percentage(PersonalityTrait.Idea),
                CompletedAtUtc = DateTimeOffset.UtcNow
            };

            await _dbContext.PersonalityTestResults.AddAsync(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PersonalityTestResultView?> GetResultAsync(Guid userId)
        {
            var saved = await _dbContext.PersonalityTestResults
                .AsNoTracking()
                .SingleOrDefaultAsync(result => result.UserId == userId);

            if (saved == null)
            {
                return null;
            }

            var scores = new[]
            {
                (Trait: PersonalityTrait.Action, Score: saved.ActionScore, Percentage: saved.ActionPercentage),
                (Trait: PersonalityTrait.Process, Score: saved.ProcessScore, Percentage: saved.ProcessPercentage),
                (Trait: PersonalityTrait.People, Score: saved.PeopleScore, Percentage: saved.PeoplePercentage),
                (Trait: PersonalityTrait.Idea, Score: saved.IdeaScore, Percentage: saved.IdeaPercentage)
            };
            var topScore = scores.Max(score => score.Score);

            return new PersonalityTestResultView
            {
                FeedbackRating = saved.FeedbackRating,
                FeedbackComment = saved.FeedbackComment,
                Scores = scores
                    .OrderByDescending(score => score.Score)
                    .Select(score => new PersonalityTraitScoreView
                    {
                        Trait = score.Trait,
                        Name = PersonalityTestConstants.TraitNames[score.Trait],
                        Description = PersonalityTestConstants.TraitDescriptions[score.Trait],
                        Score = score.Score,
                        MaxScore = MaxScore(score.Trait),
                        Percentage = score.Percentage,
                        IsStrongest = score.Score == topScore
                    })
                    .ToList()
            };
        }

        public async Task<bool> SaveFeedbackAsync(Guid userId, int rating, string? comment)
        {
            if (rating < PersonalityTestConstants.MinFeedbackRating || rating > PersonalityTestConstants.MaxFeedbackRating)
            {
                return false;
            }

            var result = await _dbContext.PersonalityTestResults.SingleOrDefaultAsync(r => r.UserId == userId);
            if (result == null)
            {
                return false;
            }

            result.FeedbackRating = rating;
            result.FeedbackComment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();
            result.FeedbackSubmittedAtUtc = DateTimeOffset.UtcNow;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private static int MaxScore(PersonalityTrait trait)
        {
            return PersonalityTestConstants.Questions
                .SelectMany(question => question.Answers)
                .Count(answer => answer.Trait == trait);
        }
    }
}
