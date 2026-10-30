namespace StudentsManager.Mvc.Domain.Entities
{
    public class RoboExaminationAttempt
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public int RoboExaminationId { get; set; }
        public RoboExamination Examination { get; set; } = null!;
        public string ExaminationType { get; set; } = string.Empty;
        public DateTime AssignedAtUtc { get; set; }
        public DateTime? SubmittedAtUtc { get; set; }
        public string? AnswersJson { get; set; }
        public RoboExaminationEvaluationStatus EvaluationStatus { get; set; }
        public DateTime? EvaluationStartedAtUtc { get; set; }
        public int? Grade { get; set; }
        public string? Feedback { get; set; }
        public string? RawEvaluation { get; set; }
        public string? EvaluationError { get; set; }
    }
}
