namespace StudentsManager.Mvc.Domain.Views.RoboExaminations
{
    public class RoboExaminationSubmissionView
    {
        public int AttemptId { get; set; }
        public DateTime? SubmittedAtUtc { get; set; }
        public string EvaluationStatus { get; set; } = string.Empty;
        public int? Grade { get; set; }
        public string? Feedback { get; set; }
    }
}
