using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboQuizzes
{
    public class RoboQuizQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; } = string.Empty;

        [Required]
        public List<RoboQuizOption> Options { get; set; } = [];

        public int CorrectAnswer { get; set; }
    }
}
