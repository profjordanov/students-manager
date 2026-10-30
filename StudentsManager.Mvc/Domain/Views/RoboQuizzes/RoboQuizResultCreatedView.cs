namespace StudentsManager.Mvc.Domain.Views.RoboQuizzes
{
    public class RoboQuizResultCreatedView
    {
        public int Id { get; set; }
        public int RoboQuizId { get; set; }
        public int ScorePercentage { get; set; }
        public DateTime CompletedAtUtc { get; set; }
    }
}
