using StudentsManager.Mvc.Domain.Entities;
using static StudentsManager.Mvc.Services.Topics.TopicsSeeder;

namespace StudentsManager.Mvc.Services.Questions
{
    public static class OnsenTopicSeeder
    {
        // Questions
        public static readonly TestQuestion OnsenQuestion1 = new()
        {
            Id = Guid.Parse("4da314cf-4e97-4c82-a7c6-5be1d556435d"),
            Description = "ons.platform, select () се извиква след като приложението се инициализира напълно?",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion2 = new()
        {
            Id = Guid.Parse("1beee5e4-7c83-4816-9e48-ec602d86e307"),
            Description = "Наличните семейства икони (icon families) в onsen са:?",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion3 = new()
        {
            Id = Guid.Parse("3fd420db-814c-4389-9da3-de8817578b45"),
            Description = "Произволни входове на компоненти (Arbitrary inputs of components) са?",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion4 = new()
        {
            Id = Guid.Parse("42001566-ee1e-477e-8c37-e46b00ee87fd"),
            Description = "Кое от изброеното е ons.platform обект?",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion5 = new()
        {
            Id = Guid.Parse("e064fc15-2c97-40a7-bc1b-cdce02bdb422"),
            Description = "Ons обектът трябва да бъде импортиран от библиотекатa:",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion6 = new()
        {
            Id = Guid.Parse("04b6a0b3-3e02-49bc-aeee-a5673df6bdfc"),
            Description = "Onsen използва различен изходен код за различни платформи?",
            TopicId = OnsenTopic.Id
        };

        public static readonly TestQuestion OnsenQuestion7 = new()
        {
            Id = Guid.Parse("cd4e3d1f-640e-4c87-a0c1-941398dfde0c"),
            Description = "Кое CLI използва Onsen за разработване на приложения?",
            TopicId = OnsenTopic.Id
        };

        public static readonly List<TestQuestion> OnsenQuestions = new()
        {
            OnsenQuestion1,
            OnsenQuestion2,
            OnsenQuestion3,
            OnsenQuestion4,
            OnsenQuestion5,
            OnsenQuestion6,
            OnsenQuestion7
        };

        // Options
        public static readonly QuestionOption OnsenQuestion1А = new()
        {
            Description = "Вярно",
            IsCorrect = false,
            IsTextOnly = false,
            TestQuestionId = OnsenQuestion1.Id,
            ImageAddress = "https://upload.wikimedia.org/wikipedia/commons/1/1c/True_Corporation_%28Thailand%29.svg"
        };

        public static readonly QuestionOption OnsenQuestion1B = new()
        {
            Description = "Грешно",
            IsCorrect = true,
            IsTextOnly = false,
            TestQuestionId = OnsenQuestion1.Id,
            ImageAddress = "https://image.shutterstock.com/image-vector/false-stampstampsignfalse-260nw-437357437.jpg"
        };

        public static readonly QuestionOption OnsenQuestion2А = new()
        {
            Description = "Само Font Awesome",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion2.Id
        };

        public static readonly QuestionOption OnsenQuestion2B = new()
        {
            Description = "Всички",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion2.Id
        };

        public static readonly QuestionOption OnsenQuestion3А = new()
        {
            Description = "Props",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion3.Id
        };

        public static readonly QuestionOption OnsenQuestion3B = new()
        {
            Description = "Keys",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion3.Id
        };

        public static readonly QuestionOption OnsenQuestion4А = new()
        {
            Description = "ons.platform.isWebView",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion4.Id
        };

        public static readonly QuestionOption OnsenQuestion4B = new()
        {
            Description = "ons.platform.isChromeView",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion4.Id
        };

        public static readonly QuestionOption OnsenQuestion5А = new()
        {
            Description = "onsplatform",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion5.Id
        };

        public static readonly QuestionOption OnsenQuestion5B = new()
        {
            Description = "onsenui",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion5.Id
        };

        public static readonly QuestionOption OnsenQuestion6А = new()
        {
            Description = "Вярно",
            IsCorrect = false,
            IsTextOnly = false,
            TestQuestionId = OnsenQuestion6.Id,
            ImageAddress = "https://upload.wikimedia.org/wikipedia/commons/1/1c/True_Corporation_%28Thailand%29.svg"
        };

        public static readonly QuestionOption OnsenQuestion6B = new()
        {
            Description = "Грешно",
            IsCorrect = true,
            IsTextOnly = false,
            TestQuestionId = OnsenQuestion6.Id,
            ImageAddress = "https://image.shutterstock.com/image-vector/false-stampstampsignfalse-260nw-437357437.jpg"
        };

        public static readonly QuestionOption OnsenQuestion7А = new()
        {
            Description = "monaca",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion7.Id
        };

        public static readonly QuestionOption OnsenQuestion7B = new()
        {
            Description = "cordova",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = OnsenQuestion7.Id
        };

        public static readonly List<QuestionOption> OnsenQuestionOptions = new()
        {
            OnsenQuestion1А,
            OnsenQuestion1B,
            OnsenQuestion2А,
            OnsenQuestion2B,
            OnsenQuestion3А,
            OnsenQuestion3B,
            OnsenQuestion4А,
            OnsenQuestion4B,
            OnsenQuestion5А,
            OnsenQuestion5B,
            OnsenQuestion6А,
            OnsenQuestion6B,
            OnsenQuestion7А,
            OnsenQuestion7B
        };
    }
}