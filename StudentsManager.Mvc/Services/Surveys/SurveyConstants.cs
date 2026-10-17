namespace StudentsManager.Mvc.Services.Surveys
{
    public static class SurveyConstants
    {
        public const string PreviousExperienceQuestion = "Have you had any previous experience with programming or informatics? If so, which languages or fields?";
        public const string CourseExpectationsQuestion = "What do you hope to gain or achieve by taking this course?";
        public const string HobbyQuestion = "What is one non-academic passion or hobby you enjoy in your free time?";
        public const string DreamProjectQuestion = "If you could work on any dream project (personal or professional), what would it be and why?";

        public const int MaxPictureSizeInBytes = 5 * 1024 * 1024;
        public const string BlobNamePrefix = "survey-";

        public static readonly IReadOnlyDictionary<string, string> PictureContentTypesByExtension =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                [".jpg"] = "image/jpeg",
                [".jpeg"] = "image/jpeg",
                [".png"] = "image/png",
                [".webp"] = "image/webp"
            };
    }
}