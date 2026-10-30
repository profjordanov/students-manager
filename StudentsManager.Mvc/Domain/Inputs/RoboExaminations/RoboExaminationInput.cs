using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboExaminations
{
    public class RoboExaminationInput : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Variant { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<RoboExaminationQuestion> Questions { get; set; } = [];

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

            if (Questions.Select(question => question?.Id).Distinct(StringComparer.Ordinal).Count() != Questions.Count)
                yield return new ValidationResult("Question IDs must be unique.", [nameof(Questions)]);

            for (var index = 0; index < Questions.Count; index++)
            {
                var question = Questions[index];
                if (question == null)
                {
                    yield return new ValidationResult($"Question {index + 1} must be an object.", [nameof(Questions)]);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(question.Id) || question.Id.Length > 50)
                    yield return new ValidationResult($"Question {index + 1} ID must contain 1–50 characters.", [nameof(Questions)]);
                if (string.IsNullOrWhiteSpace(question.Text) || question.Text.Length > 1000)
                    yield return new ValidationResult($"Question {index + 1} text must contain 1–1000 characters.", [nameof(Questions)]);
                if (question.Type != RoboExaminationQuestion.TextType)
                    yield return new ValidationResult($"Question {index + 1} type must be 'text'.", [nameof(Questions)]);
            }
        }
    }
}
