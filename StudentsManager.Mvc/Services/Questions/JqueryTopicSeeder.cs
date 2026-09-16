using StudentsManager.Mvc.Domain.Entities;
using static StudentsManager.Mvc.Services.Topics.TopicsSeeder;

namespace StudentsManager.Mvc.Services.Questions
{
    public static class JqueryTopicSeeder
    {
        public static readonly TestQuestion JqueryQuestion1 = new()
        {
            Id = Guid.Parse("ccc2cc7e-6b24-4969-a86a-d748bb951bfd"),
            Description = "Как да добавим CSS клас към елемент с помощта на jQuery?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion2 = new()
        {
            Id = Guid.Parse("0f1f35b6-64c5-4df4-a05f-46bdd05a1fc7"),
            Description = "Как да премахнем CSS клас от елемент с помощта на jQuery?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion3 = new()
        {
            Id = Guid.Parse("fb274bf4-1b7a-440c-94af-336cdd695bc9"),
            Description = "Какво представляват приставките за jQuery?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion4 = new()
        {
            Id = Guid.Parse("83e01938-1083-435f-8557-f97b1f760bc7"),
            Description = "За какво се използва метода param() ?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion5 = new()
        {
            Id = Guid.Parse("a2405d7a-c2f0-44cb-a445-18fb53a0917a"),
            Description = "Какви са методите за ефекти, използвани в jQuery?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion6 = new()
        {
            Id = Guid.Parse("d40bb459-ed20-4088-8a2e-f16e8d4f8869"),
            Description = "Какво е jQuery Mobile?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion7 = new()
        {
            Id = Guid.Parse("a0098105-b6e6-4bb4-a1b7-fb2cf906f5c1"),
            Description = "Как да включим множество страници в един мобилен документ?",
            TopicId = JqueryTopic.Id
        };

        public static readonly TestQuestion JqueryQuestion8 = new()
        {
            Id = Guid.Parse("5e189dfd-e082-4268-bb59-8726d47bf82a"),
            Description = "Как се ползват преходни ефекти в страница?",
            TopicId = JqueryTopic.Id
        };

        public static readonly List<TestQuestion> JqueryQuestions = new()
        {
            JqueryQuestion1,
            JqueryQuestion2,
            JqueryQuestion3,
            JqueryQuestion4,
            JqueryQuestion5,
            JqueryQuestion6,
            JqueryQuestion7,
            JqueryQuestion8
        };

        // Options
        public static readonly QuestionOption JqueryQuestionOption1A = new()
        {
            Description = "$(\"h1\").attachClass(\"myclass\");",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion1.Id
        };

        public static readonly QuestionOption JqueryQuestionOption1B = new()
        {
            Description = "$(\"h1\").addClass(\"myclass\");",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion1.Id
        };

        public static readonly QuestionOption JqueryQuestionOption2A = new()
        {
            Description = "$(\"h1\").removeClass(\"myclass\");",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion2.Id
        };

        public static readonly QuestionOption JqueryQuestionOption2B = new()
        {
            Description = "$(\"h1\").detachClass(\"myclass\");",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion2.Id
        };

        public static readonly QuestionOption JqueryQuestionOption3A = new()
        {
            Description = "Методи, които позволяват на разработчиците да разширят прототипните обекти на jQuery.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion3.Id
        };

        public static readonly QuestionOption JqueryQuestionOption3B = new()
        {
            Description = "Позволяват достъп до камерата, батерията и други.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion3.Id
        };

        public static readonly QuestionOption JqueryQuestionOption4A = new()
        {
            Description = "Задава параметри на обект или масив.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion4.Id
        };

        public static readonly QuestionOption JqueryQuestionOption4B = new()
        {
            Description = "Извежда сериализирано представяне на обект или масив.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion4.Id
        };

        public static readonly QuestionOption JqueryQuestionOption5A = new()
        {
            Description = "onload(), ready()",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion5.Id
        };

        public static readonly QuestionOption JqueryQuestionOption5B = new()
        {
            Description = "show(), hide(), toggle()",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion5.Id
        };

        public static readonly QuestionOption JqueryQuestionOption6A = new()
        {
            Description = "Рамка за потребителски интерфейс.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion6.Id
        };

        public static readonly QuestionOption JqueryQuestionOption6B = new()
        {
            Description = "Работна рамка за достъп до хардуерни елементи - камера, батерия.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion6.Id
        };

        public static readonly QuestionOption JqueryQuestionOption7A = new()
        {
            Description = "Добавяне на множество div с атрибут data-role = \"page\".",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion7.Id
        };

        public static readonly QuestionOption JqueryQuestionOption7B = new()
        {
            Description = "Добавяне на множество div с атрибут page-role = \"home\".",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion7.Id
        };

        public static readonly QuestionOption JqueryQuestionOption8A = new()
        {
            Description = "Чрез атрибута data-transformation",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion8.Id
        };

        public static readonly QuestionOption JqueryQuestionOption8B = new()
        {
            Description = "Чрез атрибута data-transition",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = JqueryQuestion8.Id
        };

        public static readonly List<QuestionOption> JqueryQuestionOptions = new()
        {
            JqueryQuestionOption1A,
            JqueryQuestionOption1B,
            JqueryQuestionOption2A,
            JqueryQuestionOption2B,
            JqueryQuestionOption3A,
            JqueryQuestionOption3B,
            JqueryQuestionOption4A,
            JqueryQuestionOption4B,
            JqueryQuestionOption5A,
            JqueryQuestionOption5B,
            JqueryQuestionOption6A,
            JqueryQuestionOption6B,
            JqueryQuestionOption7A,
            JqueryQuestionOption7B,
            JqueryQuestionOption8A,
            JqueryQuestionOption8B
        };
    }
}