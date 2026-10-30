namespace StudentsManager.Mvc.Domain.Entities
{
    public class RoboQuizUserResult
    {
        public int Id { get; set; }
        public int ScorePercentage { get; set; }
        public DateTime CompletedAtUtc { get; set; }

        public int RoboQuizId { get; set; }
        public RoboQuiz RoboQuiz { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
