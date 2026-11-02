using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views;
using StudentsManager.Mvc.Mappings;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.Tests
{
    public class TestsService(ManagerDbContext dbContext) : ITestsService
    {
        public async Task<TestViewModel> GetTestAsync(Guid topicId, Guid userId)
        {
            var entities = await GetTestQuestionsAsync(topicId, userId);
            var result = entities
                .Select(question => question.ToView())
                .ToList();
            return new TestViewModel(result);
        }

        public async Task<IEnumerable<TestQuestion>> GetTestQuestionsAsync(Guid topicId, Guid userId)
        {
            var alreadyAnswered = await dbContext
                .UserAnswers
                .Where(answer => answer.UserId == userId)
                .Include(answer => answer.QuestionOption)
                .ThenInclude(option => option.TestQuestion)
                .Where(answer => answer.QuestionOption.TestQuestion.TopicId == topicId)
                .ToListAsync();

            var testQuestions = await dbContext
                .TestQuestions
                .Where(question => question.TopicId == topicId)
                .Include(question => question.Options)
                .ToListAsync();

            if (alreadyAnswered.Count == 0) return testQuestions;

            var answeredQuestionIds = alreadyAnswered
                .Select(answer => answer.QuestionOption.TestQuestionId)
                .ToHashSet();

            return testQuestions.Where(question => !answeredQuestionIds.Contains(question.Id));
        }
    }
}