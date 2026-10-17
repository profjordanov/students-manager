namespace StudentsManager.Mvc.Domain.Entities
{
    public class PowerUpAnalysis
    {
        public int Id { get; set; }

        public string AnalysisResult { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
