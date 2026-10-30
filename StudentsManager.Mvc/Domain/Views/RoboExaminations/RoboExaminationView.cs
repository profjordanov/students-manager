namespace StudentsManager.Mvc.Domain.Views.RoboExaminations
{
    public class RoboExaminationView
    {
        public int AttemptId { get; set; }
        public int RoboExaminationId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public IReadOnlyList<RoboExaminationQuestionView> Questions { get; set; } = [];
    }
}
