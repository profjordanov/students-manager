namespace StudentsManager.Mvc.Domain.Entities
{
    public class PersonalityTestResult
    {
        public int Id { get; set; }

        public int ActionScore { get; set; }
        public int ProcessScore { get; set; }
        public int PeopleScore { get; set; }
        public int IdeaScore { get; set; }

        public decimal ActionPercentage { get; set; }
        public decimal ProcessPercentage { get; set; }
        public decimal PeoplePercentage { get; set; }
        public decimal IdeaPercentage { get; set; }

        public DateTimeOffset CompletedAtUtc { get; set; }

        public int? FeedbackRating { get; set; }
        public string? FeedbackComment { get; set; }
        public DateTimeOffset? FeedbackSubmittedAtUtc { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
