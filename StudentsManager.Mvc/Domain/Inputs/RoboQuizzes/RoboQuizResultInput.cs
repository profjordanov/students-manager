using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboQuizzes
{
    public class RoboQuizResultInput
    {
        public Guid UserId { get; set; }
        public int RoboQuizId { get; set; }

        [Range(0, 100)]
        public int ScorePercentage { get; set; }
    }
}
