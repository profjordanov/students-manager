namespace StudentsManager.Mvc.Domain.Views.RoboQuizzes
{
    public class RoboQuizResultView
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public int ScorePercentage { get; set; }
        public DateTime CompletedAtUtc { get; set; }
    }
}
