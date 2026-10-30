namespace StudentsManager.Mvc.Domain.Entities
{
    public class RoboExamination
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public string StringifiedJsonContent { get; set; } = string.Empty;
        public ICollection<RoboExaminationAttempt> Attempts { get; set; } = [];
    }
}
