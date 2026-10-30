using StudentsManager.Mvc.Domain.Inputs.RoboQuizzes;

namespace StudentsManager.Mvc.Domain.Views.RoboQuizzes
{
    public class RoboQuizView
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public List<RoboQuizQuestion> Questions { get; set; } = [];
    }
}
