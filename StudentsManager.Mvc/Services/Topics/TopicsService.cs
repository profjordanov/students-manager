using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Views;
using StudentsManager.Mvc.Mappings;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.Topics
{
    public class TopicsService(ManagerDbContext dbContext) : ITopicsService
    {
        public async Task<IEnumerable<UserTopicResultView>> GetUserTopicResultsAsync(Guid userId)
        {
            var userTopicIdResults = await dbContext
                .UserTopicResults
                .Where(ut => ut.UserId == userId)
                .Select(ut => ut.TopicId)
                .ToListAsync();

            var topics = await dbContext
                .Topics
                .OrderBy(topic => topic.SequenceNumber)
                .ToListAsync();

            return userTopicIdResults.Any()
                ? topics.Select(topic => topic.ToUserResult(userId,
                    userTopicIdResults.Any(id => id == topic.Id)))
                : topics.Select(topic => topic.ToUserResult(userId, false));
        }

        public Task<bool> HasUserTopicResultAsync(Guid userId, Guid topicId)
        {
            return dbContext
                .UserTopicResults
                .AnyAsync(ut => ut.TopicId == topicId && ut.UserId == userId);
        }
    }
}