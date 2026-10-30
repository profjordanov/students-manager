using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboExaminations
{
    public class RoboExaminationSubmissionInput
    {
        public Guid UserId { get; set; }
        public int AttemptId { get; set; }

        [Required]
        [MinLength(1)]
        public List<RoboExaminationAnswerInput> Answers { get; set; } = [];
    }
}
