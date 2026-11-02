using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.Homeworks;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Storage;

namespace StudentsManager.Mvc.Services.Homeworks
{
    public class HomeworksService(ManagerDbContext dbContext, IStorageService storageService) : IHomeworksService
    {
        public async Task<IReadOnlyCollection<HomeworkView>> GetByUserAsync(Guid userId)
        {
            var topics = await dbContext
                .Topics
                .OrderBy(topic => topic.SequenceNumber)
                .ToListAsync();

            var handedHomeworks = await dbContext
                .Homeworks
                .Where(homework => homework.UserId == userId)
                .ToListAsync();

            var homeworksDictionary = handedHomeworks.ToDictionary(
                homework => homework.TopicId,
                homework => homework.Link);

            return topics
                .Select(topic => MapTopicToHomeworkView(topic, homeworksDictionary))
                .ToList();
        }

        public async Task UploadAsync(Guid userId, Guid topicId, string topicTag, IFormFile? file, string repositoryLink)
        {
            var entity = new Homework
            {
                TopicId = topicId,
                UserId = userId,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            if (file == null)
            {
                entity.RepositoryLink = repositoryLink;
            }
            else
            {
                var random = new Random();
                var fileName = $"{topicTag}{random.Next(0, 100000)}-{file.FileName}";
                await storageService.UploadToContainerAsync(userId.ToString(), file.OpenReadStream(), fileName);
                entity.FileName = fileName;
                entity.Path = storageService.PathBasis + userId + "/" + fileName;
                entity.Extension = file.ContentType;
            }

            await dbContext.Homeworks.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        private static HomeworkView MapTopicToHomeworkView(Topic topic, IReadOnlyDictionary<Guid, string> homeworksDictionary)
        {
            var hasHanded = homeworksDictionary
                .Keys
                .Any(x => x == topic.Id);
            
            return new HomeworkView
            {
                TopicId = topic.Id,
                TopicDescription = topic.Description,
                VideoLinkFromPreviousYear = topic.VideoLinkFromPreviousYear,
                TopicTag = topic.Tag,
                ExerciseFileUrl = topic.ExerciseFileUrl,
                ResourcesUrl = topic.ResourcesUrl,
                HasHanded = hasHanded,
                HomeWorkPath = hasHanded ? homeworksDictionary[topic.Id] : null
            };
        }
    }
}