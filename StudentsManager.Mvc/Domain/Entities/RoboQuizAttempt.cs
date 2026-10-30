namespace StudentsManager.Mvc.Domain.Entities
{
    public class RoboQuizAttempt
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public int RoboQuizId { get; set; }
        public RoboQuiz Quiz { get; set; } = null!;
        public string ExaminationType { get; set; } = string.Empty;
        public DateTime AssignedAtUtc { get; set; }
    }
}
