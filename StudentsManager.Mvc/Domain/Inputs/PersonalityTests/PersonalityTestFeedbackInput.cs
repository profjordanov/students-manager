using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.PersonalityTests
{
    public class PersonalityTestFeedbackInput
    {
        [Required]
        [Range(1, 5)]
        public int? Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }
}
