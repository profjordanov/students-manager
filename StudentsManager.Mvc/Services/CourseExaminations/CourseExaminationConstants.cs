namespace StudentsManager.Mvc.Services.CourseExaminations
{
    public static class CourseExaminationConstants
    {
        public const string FirstType = "First";
        public const string SecondType = "Second";

        public static string? NormalizeType(string type)
        {
            var trimmedType = type.Trim();
            if (string.Equals(trimmedType, FirstType, StringComparison.OrdinalIgnoreCase))
                return FirstType;
            if (string.Equals(trimmedType, SecondType, StringComparison.OrdinalIgnoreCase))
                return SecondType;
            return null;
        }
    }
}