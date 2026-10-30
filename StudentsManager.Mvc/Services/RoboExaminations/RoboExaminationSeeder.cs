using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public static class RoboExaminationSeeder
    {
        public static void UpgradeLegacyQuestions(ManagerDbContext dbContext)
        {
            foreach (var examination in dbContext.RoboExaminations.ToList())
            {
                var legacyContent = JArray.Parse(examination.StringifiedJsonContent);
                var questions = legacyContent.ToObject<List<LegacyQuestion>>();
                if (questions == null || questions.Count == 0
                    || questions.Any(question => question == null
                        || string.IsNullOrWhiteSpace(question.Id)
                        || string.IsNullOrWhiteSpace(question.Text)
                        || question.Type is not (RoboExaminationQuestion.TextType or "options")))
                    throw new JsonSerializationException(
                        $"RoboExamination {examination.Id} contains invalid questions and cannot be upgraded.");

                if (questions.Select(question => question.Id).Distinct(StringComparer.Ordinal).Count() != questions.Count)
                    throw new JsonSerializationException(
                        $"RoboExamination {examination.Id} contains duplicate question IDs and cannot be upgraded.");

                var hasOptionsField = legacyContent
                    .Select(question => question is JObject questionObject
                        && questionObject.Property("options", StringComparison.OrdinalIgnoreCase) != null)
                    .ToList();
                var hasExpectedAnswer = legacyContent.OfType<JObject>()
                    .Any(question => question.Property("expectedAnswer", StringComparison.OrdinalIgnoreCase) != null);
                if (!hasExpectedAnswer && !questions.Select((question, index) =>
                        question.Type == "options" || hasOptionsField[index]).Any(upgrade => upgrade))
                    continue;

                var upgradedQuestions = questions.Select((question, index) =>
                {
                    var text = question.Text;
                    if (question.Type == "options")
                    {
                        var options = question.Options;
                        if (options == null || options.Count < 2
                            || options.Any(option => option == null || string.IsNullOrWhiteSpace(option.Text)))
                            throw new JsonSerializationException(
                                $"RoboExamination {examination.Id}, question {question.Id} has invalid legacy options.");

                        // Preserve the choices as context for existing free-text answers.
                        text += "\n\n" + string.Join("\n", options.Select(option => "- " + option.Text))
                            + "\n\nAnswer in your own words.";
                    }
                    else if (hasOptionsField[index] && question.Options is { Count: > 0 })
                    {
                        throw new JsonSerializationException(
                            $"RoboExamination {examination.Id}, question {question.Id} is a text question with legacy options.");
                    }

                    return new RoboExaminationQuestion
                    {
                        Id = question.Id,
                        Text = text,
                        Type = RoboExaminationQuestion.TextType
                    };
                }).ToList();

                examination.StringifiedJsonContent = JsonConvert.SerializeObject(
                    upgradedQuestions,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }

            dbContext.SaveChanges();
        }

        private sealed class LegacyQuestion : RoboExaminationQuestion
        {
            public List<LegacyOption>? Options { get; set; }
        }

        private sealed class LegacyOption
        {
            public string Text { get; set; } = string.Empty;
        }

        public static RoboExamination FirstVariantA => new()
        {
            Type = "First",
            Variant = "A",
            StringifiedJsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(
            new List<RoboExaminationQuestion>
            {
                new RoboExaminationQuestion
                {
                    Id = "q1",
                    Text = "Which array methods do not modify the original array? Name at least one and explain how it works.",
                    Type = RoboExaminationQuestion.TextType
                },
                new RoboExaminationQuestion
                {
                    Id = "q2",
                    Text = "What is the purpose of the let keyword?",
                    Type = RoboExaminationQuestion.TextType
                },
                new RoboExaminationQuestion
                {
                    Id = "q3",
                    Text = "What is functional programming?",
                    Type = RoboExaminationQuestion.TextType
                },
                new RoboExaminationQuestion
                {
                    Id = "q4",
                    Text = "Is JavaScript a case-sensitive language? Give an example.",
                    Type = RoboExaminationQuestion.TextType
                },
                new RoboExaminationQuestion
                {
                    Id = "q5",
                    Text = "What is the difference between == and === operators?",
                    Type = RoboExaminationQuestion.TextType
                },
                new RoboExaminationQuestion
                {
                    Id = "q6",
                    Text = "What is an IIFE (Immediately Invoked Function Expression)?",
                    Type = RoboExaminationQuestion.TextType
                }
            },
            Newtonsoft.Json.Formatting.None,
            new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            })
        };
    }
}
