using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboExaminations
{
    public class RoboExaminationAnswerInput
    {
        [Required]
        [StringLength(50)]
        public string QuestionId { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Answer { get; set; } = string.Empty;
    }
}
