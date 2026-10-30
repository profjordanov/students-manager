using System.Reflection;
using Marvin.StreamExtensions;
using Newtonsoft.Json;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Click;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Messaging;
using UserAnswer = StudentsManager.Mvc.Domain.Inputs.Click.UserAnswer;

namespace StudentsManager.Mvc.Services.Clicks
{
    public class ClicksService : IClicksService
    {
        private readonly ManagerDbContext _managerDbContext;
        private readonly IAzureServiceBusSender _serviceBusSender;

        public ClicksService(ManagerDbContext managerDbContext, IAzureServiceBusSender serviceBusSender)
        {
            _managerDbContext = managerDbContext;
            _serviceBusSender = serviceBusSender;
        }

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
                var _ = JsonConvert.DeserializeObject<IEnumerable<UserAnswer>>(rootObject.Res);
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
                await _managerDbContext.AddAsync(entity);
                await _managerDbContext.SaveChangesAsync();

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