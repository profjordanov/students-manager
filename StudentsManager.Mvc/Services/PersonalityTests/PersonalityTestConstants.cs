using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.PersonalityTests;

namespace StudentsManager.Mvc.Services.PersonalityTests
{
    public static class PersonalityTestConstants
    {
        public const int MinFeedbackRating = 1;
        public const int MaxFeedbackRating = 5;

        public const string QuestionPrompt = "Избери едно от двете твърдения, което в по-голяма степен се отнася за теб.";

        public static readonly IReadOnlyDictionary<PersonalityTrait, string> TraitNames =
            new Dictionary<PersonalityTrait, string>
            {
                [PersonalityTrait.Action] = "Действие",
                [PersonalityTrait.Process] = "Процес",
                [PersonalityTrait.People] = "Хора",
                [PersonalityTrait.Idea] = "Идеи"
            };

        public static readonly IReadOnlyDictionary<PersonalityTrait, string> TraitDescriptions =
            new Dictionary<PersonalityTrait, string>
            {
                [PersonalityTrait.Action] = "Насочваш се към ясен резултат и предпочиташ бързо да преминаваш от решение към действие. Цениш практичните подходи и видимия напредък.",
                [PersonalityTrait.Process] = "Подхождаш обмислено и систематично, като се опираш на факти и ясна логика. Цениш добрия план, последователността и вниманието към детайла.",
                [PersonalityTrait.People] = "Обръщаш внимание на хората и се стремиш да разбираш гледните им точки. Цениш откритата комуникация, взаимното доверие и съвместната работа.",
                [PersonalityTrait.Idea] = "Забелязваш нови възможности и обичаш да търсиш нестандартни решения. Цениш разнообразието, въображението и свободата да експериментираш."
            };

        public static readonly IReadOnlyList<PersonalityTestQuestion> Questions =
        [
            new(1, [new("Действам", PersonalityTrait.Action, "1.jpg"),
                    new("Намирам решение", PersonalityTrait.Process, "2.jpg")]),
            new(2, [new("„Сговорна дружина планина повдига“", PersonalityTrait.People),
                    new("Често ми светва лампичката", PersonalityTrait.Idea)]),
            new(3, [new("Мечтател съм", PersonalityTrait.Idea, "5.jpg"),
                    new("Общителен съм", PersonalityTrait.People, "6.jpg")]),
            new(4, [new("Харесва ми, когато срещите са добре организирани.", PersonalityTrait.Process),
                    new("Винаги спазвам крайните срокове", PersonalityTrait.Action)]),
            new(5, [new("„Не оставям днешната работа за утре“", PersonalityTrait.Action),
                    new("„Три пъти мери, един път режи“", PersonalityTrait.Process)]),
            new(6, [new("Душата на компанията съм", PersonalityTrait.People),
                    new("Винаги търся нови възможности.", PersonalityTrait.Idea)]),
            new(7, [new("Сам съм си шеф", PersonalityTrait.Action),
                    new("Обичам да правя всичко от игла до конец", PersonalityTrait.Process)]),
            new(8, [new("Грижа ме е за чувствата на другите", PersonalityTrait.People),
                    new("Обичам да провокирам и да наблюдавам реакциите на другите", PersonalityTrait.Idea)]),
            new(9, [new("„По дрехите посрещат, а по ума изпращат“", PersonalityTrait.Action),
                    new("„Капка по капка вир прави“", PersonalityTrait.Process)]),
            new(10, [new("Комуникативен съм", PersonalityTrait.People, "19.jpg"),
                     new("Креативен съм", PersonalityTrait.Idea, "20.jpg")]),
            new(11, [new("„Къща без основа не се строи“", PersonalityTrait.Idea),
                     new("„Другар в нужда се познава“", PersonalityTrait.People)]),
            new(12, [new("Планирам", PersonalityTrait.Process, "23.jpg"),
                     new("Действам по същество", PersonalityTrait.Action, "24.jpg")]),
            new(13, [new("Умея да се справям с напрежението.", PersonalityTrait.Process),
                     new("Съвършенството се постига с практика.", PersonalityTrait.Action)]),
            new(14, [new("Добър слушател съм.", PersonalityTrait.People),
                     new("Мисля бързо.", PersonalityTrait.Idea)]),
            new(15, [new("Екипен играч съм.", PersonalityTrait.People),
                     new("Имам логическо мислене.", PersonalityTrait.Process)]),
            new(16, [new("Мултитаскинг", PersonalityTrait.Action, "31.jpg"),
                     new("Обмислям внимателно", PersonalityTrait.Idea, "32.jpg")]),
            new(17, [new("Скачайки в дълбокото, уча най-добре.", PersonalityTrait.Action),
                     new("Логика > Емоции.", PersonalityTrait.Process)]),
            new(18, [new("Хората са като отворена книга за мен.", PersonalityTrait.People),
                     new("Не обичам детайлите.", PersonalityTrait.Idea)]),
            new(19, [new("Първо анализирам, после действам.", PersonalityTrait.Process),
                     new("Умея да водя работни срещи.", PersonalityTrait.People)]),
            new(20, [new("„И утре е ден“.", PersonalityTrait.Idea),
                     new("„Казана дума, хвърлен камък“.", PersonalityTrait.Action)]),
            new(21, [new("Авантюрист съм.", PersonalityTrait.Action, "41.jpg"),
                     new("Изследовател съм", PersonalityTrait.Process, "42.jpg")]),
            new(22, [new("Открит човек съм.", PersonalityTrait.People),
                     new("Иновативен съм.", PersonalityTrait.Idea)]),
            new(23, [new("Обичам да чета.", PersonalityTrait.Idea, "45.jpg"),
                     new("Решавам проблеми.", PersonalityTrait.People, "46.jpg")]),
            new(24, [new("Концентриран/а съм.", PersonalityTrait.Process),
                     new("За мен е важен крайният резултат.", PersonalityTrait.Action)]),
            new(25, [new("Обичам да опознавам хората.", PersonalityTrait.People),
                     new("Обичам разнообразието.", PersonalityTrait.Action)]),
            new(26, [new("Доверявам се на фактите.", PersonalityTrait.Process, "51.jpg"),
                     new("Използвам въображението си.", PersonalityTrait.Idea, "52.jpg")]),
            new(27, [new("Губя търпение при бавни процеси.", PersonalityTrait.Action),
                     new("Имам хиляди мисли в главата си.", PersonalityTrait.Idea)]),
            new(28, [new("Не обичам прибързаните решения.", PersonalityTrait.Process),
                     new("Екипната работа води до прогрес.", PersonalityTrait.People)]),
            new(29, [new("Не се замислям два пъти.", PersonalityTrait.Action),
                     new("Не взимам емоционални решения.", PersonalityTrait.Process)]),
            new(30, [new("Важно ми е да съм харесван.", PersonalityTrait.People),
                     new("Правя правилните заключения.", PersonalityTrait.Idea)]),
            new(31, [new("Търся странично мнение.", PersonalityTrait.Idea),
                     new("Следвам определена методология.", PersonalityTrait.Process)]),
            new(32, [new("Обичам да свършвам работата.", PersonalityTrait.Action),
                     new("Добрите взаимоотношения са важни.", PersonalityTrait.People)]),
            new(33, [new("Импулсивен/импулсивна съм.", PersonalityTrait.Action),
                     new("Широкоскроен/а съм.", PersonalityTrait.Process)]),
            new(34, [new("„Блага дума железни врати отваря“.", PersonalityTrait.People),
                     new("Обичам трудните задачи.", PersonalityTrait.Idea)]),
            new(35, [new("Организиран съм.", PersonalityTrait.Process, "69.jpg"),
                     new("Скачам от задача на задача", PersonalityTrait.Action, "70.jpg")]),
            new(36, [new("Комуникирам без проблем с различни хора.", PersonalityTrait.People),
                     new("Стремя се към личностно развитие.", PersonalityTrait.Idea)]),
            new(37, [new("Идеен съм.", PersonalityTrait.Idea),
                     new("Не обичам да си губя времето.", PersonalityTrait.Action)]),
            new(38, [new("„Обичам да плавам в собствени води“.", PersonalityTrait.Process),
                     new("За мен е важно да имам добър ментор.", PersonalityTrait.People)]),
            new(39, [new("Мисля абстрактно.", PersonalityTrait.Idea, "77.jpg"),
                     new("Фокусиран/а съм в детайлите", PersonalityTrait.Process, "78.jpg")]),
            new(40, [new("Не обичам разтягането на локуми.", PersonalityTrait.Action),
                     new("Вярвам в себе си.", PersonalityTrait.People)])
        ];
    }
}
