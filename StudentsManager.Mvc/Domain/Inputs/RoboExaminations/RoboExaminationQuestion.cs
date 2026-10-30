namespace StudentsManager.Mvc.Domain.Inputs.RoboExaminations
{
    public class RoboExaminationQuestion
    {
        public const string TextType = "text";

        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
