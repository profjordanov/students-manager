using System.Reflection;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Click;
using StudentsManager.Mvc.Persistence;
using UserAnswer = StudentsManager.Mvc.Domain.Inputs.Click.UserAnswer;

namespace StudentsManager.Mvc.Services.Clicks
{
    public class ClicksService(ManagerDbContext managerDbContext) : IClicksService
    {
        public object GetQuestionsContent(string type)
        {
            const string resourcePath = "StudentsManager.Mvc.Resources.";

            var manifestName = resourcePath + "resource.json";

            if (!string.IsNullOrEmpty(type) && type == "beginner") manifestName = resourcePath + "basics.json";

            var questionsResourceStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(manifestName);

            return questionsResourceStream.ReadAndDeserializeFromJson();
        }

        public async Task SaveAndPublishAnswersAsync(ClickResultsRootObject rootObject, Guid userId, string questionsContentType)
        {
            await SaveAnswersAsync(rootObject, userId, questionsContentType);
        }

        public async Task<ExaminationAnswer> SaveAnswersAsync(ClickResultsRootObject rootObject, Guid userId, string questionsContentType)
        {
            try
            {
                _ = JsonConvert.DeserializeObject<IEnumerable<UserAnswer>>(rootObject.Res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            try
            {
                var entity = new ExaminationAnswer
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    Result = rootObject.Res,
                    Form = rootObject.Form,
                    ContentType = questionsContentType
                };
                await managerDbContext.AddAsync(entity);
                await managerDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}