using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.RoboQuizzes
{
    public class RoboQuizOption
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
