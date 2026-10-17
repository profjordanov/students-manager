using System.ComponentModel.DataAnnotations;
using StudentsManager.Mvc.Services.Surveys;

namespace StudentsManager.Mvc.Domain.Inputs.Surveys
{
    public class SurveySubmission : IValidatableObject
    {
        [Required]
        [StringLength(2000)]
        [Display(Name = SurveyConstants.PreviousExperienceQuestion)]
        public string? PreviousExperience { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = SurveyConstants.CourseExpectationsQuestion)]
        public string? CourseExpectations { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = SurveyConstants.HobbyQuestion)]
        public string? Hobby { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = SurveyConstants.DreamProjectQuestion)]
        public string? DreamProject { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public IFormFile? Picture { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Picture == null)
            {
                yield break;
            }

            if (Picture.Length == 0)
            {
                yield return new ValidationResult("Please upload a non-empty picture.", new[] { nameof(Picture) });
            }
            else if (Picture.Length > SurveyConstants.MaxPictureSizeInBytes)
            {
                yield return new ValidationResult("The picture must be 5 MB or smaller.", new[] { nameof(Picture) });
            }

            var extension = Path.GetExtension(Picture.FileName);
            if (!SurveyConstants.PictureContentTypesByExtension.TryGetValue(extension, out var contentType) ||
                !string.Equals(Picture.ContentType, contentType, StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("Please upload a JPG, PNG or WEBP picture.", new[] { nameof(Picture) });
            }
        }
    }
}