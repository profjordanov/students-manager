namespace StudentsManager.Mvc.Domain.Views.Surveys
{
    public class SurveyResultView
    {
        public string FullName { get; set; } = string.Empty;
        public string PreviousExperience { get; set; } = string.Empty;
        public string CourseExpectations { get; set; } = string.Empty;
        public string Hobby { get; set; } = string.Empty;
        public string DreamProject { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public DateTimeOffset CreatedAtUtc { get; set; }
    }
}