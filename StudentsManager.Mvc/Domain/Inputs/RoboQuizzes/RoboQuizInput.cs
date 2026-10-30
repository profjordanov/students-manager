using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboQuizzes
{
    public class RoboQuizInput : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Variant { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<RoboQuizQuestion> Questions { get; set; } = [];

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Type))
                yield return new ValidationResult("Type is required.", [nameof(Type)]);

            if (string.IsNullOrWhiteSpace(Variant))
                yield return new ValidationResult("Variant is required.", [nameof(Variant)]);

            if (Questions == null || Questions.Count == 0)
            {
                yield return new ValidationResult("At least one question is required.", [nameof(Questions)]);
                yield break;
            }

            if (Questions.Where(question => question != null).Select(question => question.Id).Distinct().Count()
                != Questions.Count(question => question != null))
                yield return new ValidationResult("Question IDs must be unique.", [nameof(Questions)]);

            for (var questionIndex = 0; questionIndex < Questions.Count; questionIndex++)
            {
                var question = Questions[questionIndex];
                if (question == null)
                {
                    yield return new ValidationResult($"Question {questionIndex + 1} must be an object.", [nameof(Questions)]);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(question.Question))
                    yield return new ValidationResult($"Question {questionIndex + 1} must have text.", [nameof(Questions)]);

                if (question.Options == null || question.Options.Count < 2)
                {
                    yield return new ValidationResult($"Question {questionIndex + 1} must have at least two options.", [nameof(Questions)]);
                    continue;
                }

                if (question.Options.Any(option => option == null))
                {
                    yield return new ValidationResult($"Question {questionIndex + 1} options must be objects.", [nameof(Questions)]);
                    continue;
                }

                if (question.Options.Any(option => string.IsNullOrWhiteSpace(option.Text)))
                    yield return new ValidationResult($"Question {questionIndex + 1} options must have text.", [nameof(Questions)]);

                if (question.Options.Select(option => option.Id).Distinct().Count() != question.Options.Count)
                    yield return new ValidationResult($"Question {questionIndex + 1} option IDs must be unique.", [nameof(Questions)]);

                if (!question.Options.Any(option => option.Id == question.CorrectAnswer))
                    yield return new ValidationResult($"Question {questionIndex + 1} correctAnswer must match an option ID.", [nameof(Questions)]);
            }
        }
    }
}
