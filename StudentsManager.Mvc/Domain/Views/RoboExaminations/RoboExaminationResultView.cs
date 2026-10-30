namespace StudentsManager.Mvc.Domain.Views.RoboExaminations
{
    public class RoboExaminationResultView
    {
        public int AttemptId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public DateTime? SubmittedAtUtc { get; set; }
        public string EvaluationStatus { get; set; } = string.Empty;
        public int? Grade { get; set; }
        public string? Feedback { get; set; }
        public IReadOnlyList<RoboExaminationAnswerView> Answers { get; set; } = [];
    }
}
