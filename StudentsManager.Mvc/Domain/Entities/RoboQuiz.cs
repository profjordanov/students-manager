namespace StudentsManager.Mvc.Domain.Entities
{
    public class RoboQuiz
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public string StringifiedJsonContent { get; set; } = string.Empty;
        public ICollection<RoboQuizUserResult>? Results { get; set; }
    }
}
