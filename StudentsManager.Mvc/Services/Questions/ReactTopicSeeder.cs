using StudentsManager.Mvc.Domain.Entities;
using static StudentsManager.Mvc.Services.Topics.TopicsSeeder;

namespace StudentsManager.Mvc.Services.Questions
{
    public static class ReactTopicSeeder
    {
        // questions
        public static readonly TestQuestion ReactQuestion1 = new()
        {
            Id = Guid.Parse("c39e9201-4e71-4257-b4d3-2701e21bb664"),
            Description = "Кое е характеристика на React?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion2 = new()
        {
            Id = Guid.Parse("2a917cdf-0119-4770-8a03-c0567734ac98"),
            Description = "Браузърите могат да четат JSX?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion3 = new()
        {
            Id = Guid.Parse("add506cc-1328-40bc-a4ff-2d3997da491d"),
            Description = "Какво е състояние (state) в React?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion4 = new()
        {
            Id = Guid.Parse("bec506cc-1328-40bc-a4ff-2d3997da4929"),
            Description = "Какво е събитие в React?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion5 = new()
        {
            Id = Guid.Parse("a3856d71-fed6-489a-be7e-0dfb0e876e84"),
            Description = "Кой метод можете да използвате, за да направите състоянието на " +
                          "компонента да зависи от промените в неговия реквизит с течение на времето?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion6 = new()
        {
            Id = Guid.Parse("4676d680-6dda-4697-a409-47b3971af158"),
            Description =
                "Кои два атрибута дефинирате в елемент <input />, за да го накарате да използва \"контролирания компонент\"?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion7 = new()
        {
            Id = Guid.Parse("01d3ce3f-a9ec-455d-9966-9d924ef62ea5"),
            Description = "Каква е разликата между контейнер и презентационни компоненти?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion8 = new()
        {
            Id = Guid.Parse("6dd159c8-ef39-4609-91bb-01e38e552747"),
            Description = "Каква е целта на декларирането на опора по подразбиране (default prop)?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion9 = new()
        {
            Id = Guid.Parse("8a840ae8-6daa-4e35-812b-3e8def34e00a"),
            Description = "Кой е валиден начин за итерация на масив в JSX?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion10 = new()
        {
            Id = Guid.Parse("4e6460e1-e128-414b-a11e-7b89ac0c5517"),
            Description = "Кой ред монтира React приложение в браузър?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion11 = new()
        {
            Id = Guid.Parse("60c203b6-4839-48ee-8d87-1d0578dd7428"),
            Description = "Кой случай на употреба е най-подходящ за React?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion12 = new()
        {
            Id = Guid.Parse("76c6ed8b-385a-4329-9449-57f67e063c10"),
            Description = "Какъв е резултата от: const App = () => (<div>{4 + 2}</div>);?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion13 = new()
        {
            Id = Guid.Parse("419fd530-0b18-49a9-8645-4e16de19b183"),
            Description = "Коя функция може да получи стойността на React контекстния обект?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion14 = new()
        {
            Id = Guid.Parse("13c9d0e9-7128-4cec-abe2-dd77d2773481"),
            Description = "Коя ключова дума е валидно вградено събитие в React?",
            TopicId = ReactTopic.Id //
        };

        public static readonly TestQuestion ReactQuestion15 = new()
        {
            Id = Guid.Parse("090b40e8-8a56-484d-bf10-ffb3dea99cec"),
            Description = "Каква е целта на PropTypes?",
            TopicId = ReactTopic.Id
        };

        public static readonly TestQuestion ReactQuestion16 = new()
        {
            Id = Guid.Parse("1075a105-75d2-41b3-a74f-5dca2b37e4e2"),
            Description = "Като се има предвид компонент с име User, кое от изброените е валиден пример за " +
                          "предаване на булева променлива, наречена userIsActive, на isActive prop в JSX?",
            TopicId = ReactTopic.Id
        };

        public static readonly List<TestQuestion> TestQuestions = new()
        {
            ReactQuestion1,
            ReactQuestion2,
            ReactQuestion3,
            ReactQuestion4,
            ReactQuestion5,
            ReactQuestion6,
            ReactQuestion7,
            ReactQuestion8,
            ReactQuestion9,

            ReactQuestion10,
            ReactQuestion11,
            ReactQuestion12,
            ReactQuestion13,
            ReactQuestion14,
            ReactQuestion15,
            ReactQuestion16
        };

        // options
        public static readonly QuestionOption ReactQuestion1А = new()
        {
            Description = "Dependency Injection",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion1.Id
        };

        public static readonly QuestionOption ReactQuestion1B = new()
        {
            Description = "Uni-Directional",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion1.Id
        };

        public static readonly QuestionOption ReactQuestion2А = new()
        {
            Description = "Вярно",
            IsCorrect = false,
            IsTextOnly = false,
            TestQuestionId = ReactQuestion2.Id,
            ImageAddress = "https://upload.wikimedia.org/wikipedia/commons/1/1c/True_Corporation_%28Thailand%29.svg"
        };

        public static readonly QuestionOption ReactQuestion2B = new()
        {
            Description = "Грешно",
            IsCorrect = true,
            IsTextOnly = false,
            TestQuestionId = ReactQuestion2.Id,
            ImageAddress = "https://image.shutterstock.com/image-vector/false-stampstampsignfalse-260nw-437357437.jpg"
        };

        public static readonly QuestionOption ReactQuestion3А = new()
        {
            Description = "Използва за вътрешна комуникация вътре в компонент.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion3.Id
        };

        public static readonly QuestionOption ReactQuestion3B = new()
        {
            Description = "Вход към компонент.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion3.Id
        };

        public static readonly QuestionOption ReactQuestion4А = new()
        {
            Description = "Използва за вътрешна комуникация в компонент.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion4.Id
        };

        public static readonly QuestionOption ReactQuestion4B = new()
        {
            Description = "Действие, което потребител може да предизвика.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion4.Id
        };

        public static readonly QuestionOption ReactQuestion5А = new()
        {
            Description = "componentDidMount",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion5.Id
        };

        public static readonly QuestionOption ReactQuestion5B = new()
        {
            Description = "getDerivedStateFromProps",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion5.Id
        };

        public static readonly QuestionOption ReactQuestion6А = new()
        {
            Description = "defaultValue, defaultChange",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion6.Id
        };

        public static readonly QuestionOption ReactQuestion6B = new()
        {
            Description = "value, onChange",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion6.Id
        };

        public static readonly QuestionOption ReactQuestion7А = new()
        {
            Description = "Презентационните компоненти са за това как изглеждат нещата, " +
                          "докато компонентите за контейнери са за това как работят нещата.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion7.Id
        };

        public static readonly QuestionOption ReactQuestion7B = new()
        {
            Description = "Презентационните компоненти предават данни и логика на компонентите на контейнера.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion7.Id
        };

        public static readonly QuestionOption ReactQuestion8А = new()
        {
            Description = "Указва стойността, която да се използва, ако не е посочен.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion8.Id
        };

        public static readonly QuestionOption ReactQuestion8B = new()
        {
            Description = "Определя DOM елемента, който компонентът трябва да използва.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion8.Id
        };

        public static readonly QuestionOption ReactQuestion9А = new()
        {
            Description = "users.map( user => {return <li>{user.name}</li>;}",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion9.Id
        };

        public static readonly QuestionOption ReactQuestion9B = new()
        {
            Description = "users.iterate( user => {return <li>{user.name}</li>;}",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion9.Id
        };

        public static readonly QuestionOption ReactQuestion10А = new()
        {
            Description = "React.createApp()",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion10.Id
        };

        public static readonly QuestionOption ReactQuestion10B = new()
        {
            Description = "ReactDOM.render()",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion10.Id
        };

        public static readonly QuestionOption ReactQuestion11А = new()
        {
            Description = "Приложение със статично съдържание от страна на сървъра.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion11.Id
        };

        public static readonly QuestionOption ReactQuestion11B = new()
        {
            Description = "Приложение с много потребителски взаимодействия и промени в потребителския интерфейс.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion11.Id
        };

        public static readonly QuestionOption ReactQuestion12А = new()
        {
            Description = "<div>6</div>",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion12.Id
        };

        public static readonly QuestionOption ReactQuestion12B = new()
        {
            Description = "<div>42</div>",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion12.Id
        };

        public static readonly QuestionOption ReactQuestion13А = new()
        {
            Description = "useContext()",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion13.Id
        };

        public static readonly QuestionOption ReactQuestion13B = new()
        {
            Description = "setContext()",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion13.Id
        };

        public static readonly QuestionOption ReactQuestion14А = new()
        {
            Description = "onDelete",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion14.Id
        };

        public static readonly QuestionOption ReactQuestion14B = new()
        {
            Description = "onSubmit",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion14.Id
        };

        public static readonly QuestionOption ReactQuestion15А = new()
        {
            Description = "За да укажете типове данни и задължителни полета за компонентни.",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion15.Id
        };

        public static readonly QuestionOption ReactQuestion15B = new()
        {
            Description = "За да зададете стойности по подразбиране за състоянията на компонентите.",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion15.Id
        };

        public static readonly QuestionOption ReactQuestion16А = new()
        {
            Description = "<User isActive=\"userIsActive\" />",
            IsCorrect = false,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion16.Id
        };

        public static readonly QuestionOption ReactQuestion16B = new()
        {
            Description = "<User isActive={userIsActive} />",
            IsCorrect = true,
            IsTextOnly = true,
            TestQuestionId = ReactQuestion16.Id
        };

        public static readonly List<QuestionOption> QuestionOptions = new()
        {
            ReactQuestion1А,
            ReactQuestion1B,
            ReactQuestion2А,
            ReactQuestion2B,
            ReactQuestion3А,
            ReactQuestion3B,
            ReactQuestion4А,
            ReactQuestion4B,
            ReactQuestion5А,
            ReactQuestion5B,
            ReactQuestion6А,
            ReactQuestion6B,
            ReactQuestion7А,
            ReactQuestion7B,
            ReactQuestion8А,
            ReactQuestion8B,
            ReactQuestion9А,
            ReactQuestion9B,

            ReactQuestion10А,
            ReactQuestion10B,
            ReactQuestion11А,
            ReactQuestion11B,
            ReactQuestion12А,
            ReactQuestion12B,
            ReactQuestion13А,
            ReactQuestion13B,
            ReactQuestion14А,
            ReactQuestion14B,
            ReactQuestion15А,
            ReactQuestion15B,
            ReactQuestion16А,
            ReactQuestion16B
        };
    }
}