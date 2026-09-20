using StudentsManager.Mvc.Domain.Entities;
using static StudentsManager.Mvc.Services.Topics.TopicsSeeder;

namespace StudentsManager.Mvc.Services.Questions
{
    public static class CordovaTopicSeeder
    {
        // Questions
        public static readonly TestQuestion CordovaIntroQuestion1 = new()
        {
            Id = Guid.Parse("5c7bcbcc-fce7-4b26-a0e9-ae237d075f4a"),
            Description =
                "Кой файл предоставя информация за приложението и посочва параметри, влияещи върху това как работи?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion2 = new()
        {
            Id = Guid.Parse("2f0dcb87-641c-453a-8239-3393e42d38ed"),
            Description = "Какво е типичното име на файла на стартовата страница на приложение Cordova?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion3 = new()
        {
            Id = Guid.Parse("03296d60-65c0-40cf-9119-ac796730dda5"),
            Description =
                "Каква технология използва Cordova за разработване на мобилни приложения на различни платформи?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion4 = new()
        {
            Id = Guid.Parse("ac284e3c-dd4e-4a3b-9b01-fcaa6b9f5b29"),
            Description = "Коя версия представя CLI?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion5 = new()
        {
            Id = Guid.Parse("473f32c5-9beb-4663-8664-bf6139ba72ad"),
            Description = "Кое от двете представлява събитие в Cordova?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion6 = new()
        {
            Id = Guid.Parse("95916bbd-e70e-410d-ad7a-3d2a53eff335"),
            Description = "Какво представлява Cordova Plugman?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion7 = new()
        {
            Id = Guid.Parse("43935bec-e2f9-4855-9719-e2cce5b171a1"),
            Description = "Каква е разликата между Phonegap и Cordova?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion8 = new()
        {
            Id = Guid.Parse("50ad5add-b3a4-44a4-84ca-589973da469d"),
            Description = "Кое от посочените е от елементите във файла config.xml на Кордова?",
            TopicId = CordovaIntroTopic.Id
        };

        public static readonly TestQuestion CordovaIntroQuestion9 = new()
        {
            Id = Guid.Parse("4fefba9c-bc7c-4257-ac9c-a05562952b25"),
            Description = "Как може Cordova да манипулира файловата система?",
            TopicId = CordovaIntroTopic.Id
        };

        // Options
        public static readonly QuestionOption CordovaIntroQuestionOption1A = new()
        {
            Description = "index.html",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion1.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption1B = new()
        {
            Description = "config.xml",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion1.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption2A = new()
        {
            Description = "index.html",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion2.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption2B = new()
        {
            Description = "config.xml",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion2.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption3A = new()
        {
            Description = "Web",
            IsCorrect = true,
            IsTextOnly = false,
            TestQuestionId = CordovaIntroQuestion3.Id,
            ImageAddress = "https://cdn.pixabay.com/photo/2015/11/16/16/41/web-1045994_960_720.jpg"
        };

        public static readonly QuestionOption CordovaIntroQuestionOption3B = new()
        {
            Description = "JVM",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion3.Id,
            ImageAddress = "https://thebeginnerspoint.com/wp-content/uploads/2017/08/maxresdefault.jpg"
        };

        public static readonly QuestionOption CordovaIntroQuestionOption4A = new()
        {
            Description = "3.0",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion4.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption4B = new()
        {
            Description = "1.0",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion4.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption5A = new()
        {
            Description = "charger plugged",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion5.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption5B = new()
        {
            Description = "Volumeup button",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion5.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption6A = new()
        {
            Description = "Уебсайт с информация за възможните приставки.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion6.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption6B = new()
        {
            Description = "Инструмент, който се използва за инсталиране и управление на приставки.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion6.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption7A = new()
        {
            Description = "Apache Cordova е като двигател, който задвижва PhoneGap.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion7.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption7B = new()
        {
            Description = "PhoneGap е като двигател, който задвижва Apache Cordova.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion7.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption8A = new()
        {
            Description = "entrypoint",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion8.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption8B = new()
        {
            Description = "widget",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion8.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption9A = new()
        {
            Description = "Чрез настройка в index.html файла",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion9.Id
        };

        public static readonly QuestionOption CordovaIntroQuestionOption9B = new()
        {
            Description = "Чрез инсталиране на приставка",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = CordovaIntroQuestion9.Id
        };

        public static readonly List<TestQuestion> CordovaIntroQuestions = new()
        {
            CordovaIntroQuestion1,
            CordovaIntroQuestion2,
            CordovaIntroQuestion3,
            CordovaIntroQuestion4,
            CordovaIntroQuestion5,
            CordovaIntroQuestion6,
            CordovaIntroQuestion7,
            CordovaIntroQuestion8,
            CordovaIntroQuestion9
        };

        public static readonly List<QuestionOption> CordovaIntroQuestionOptions = new()
        {
            CordovaIntroQuestionOption1A,
            CordovaIntroQuestionOption1B,
            CordovaIntroQuestionOption2A,
            CordovaIntroQuestionOption2B,
            CordovaIntroQuestionOption3A,
            CordovaIntroQuestionOption3B,
            CordovaIntroQuestionOption4A,
            CordovaIntroQuestionOption4B,
            CordovaIntroQuestionOption5A,
            CordovaIntroQuestionOption5B,
            CordovaIntroQuestionOption6A,
            CordovaIntroQuestionOption6B,
            CordovaIntroQuestionOption7A,
            CordovaIntroQuestionOption7B,
            CordovaIntroQuestionOption8A,
            CordovaIntroQuestionOption8B,
            CordovaIntroQuestionOption9A,
            CordovaIntroQuestionOption9B
        };
    }
}