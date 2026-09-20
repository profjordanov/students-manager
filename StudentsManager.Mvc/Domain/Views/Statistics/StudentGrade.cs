namespace StudentsManager.Mvc.Domain.Views.Statistics
{
    public class StudentGrade
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentFacultyNumber { get; set; }

        public float IntroScore { get; set; }
        public string? IntroHomework { get; set; }
        public int IntroHomeworkScore => string.IsNullOrEmpty(IntroHomework) ? 0 : 1;

        public float AdvanceScore { get; set; }
        public string? AdvanceHomework { get; set; }
        public int AdvanceHomeworkScore => string.IsNullOrEmpty(AdvanceHomework) ? 0 : 1;

        public float AsyncScore { get; set; }
        public string? AsyncHomework { get; set; }
        public int AsyncHomeworkScore => string.IsNullOrEmpty(AsyncHomework) ? 0 : 1;

        public float CordovaScore { get; set; }
        public string? CordovaHomework { get; set; }
        public int CordovaHomeworkScore => string.IsNullOrEmpty(CordovaHomework) ? 0 : 1;

        public float ReactScore { get; set; }
        public string? ReactHomework { get; set; }
        public int ReactHomeworkScore => string.IsNullOrEmpty(ReactHomework) ? 0 : 1;

        public float NativeScore { get; set; }
        public string? NativeHomework { get; set; }
        public int NativeHomeworkScore => string.IsNullOrEmpty(NativeHomework) ? 0 : 1;

        public float ArchitectureScore { get; set; }
        public string? ArchitectureHomework { get; set; }
        public int ArchitectureHomeworkScore => string.IsNullOrEmpty(ArchitectureHomework) ? 0 : 1;

        public float FirstExaminationScore { get; set; }
        public float SecondExaminationScore { get; set; }

        public bool HasExaminationAnswer { get; set; }
        public bool HasCoursework { get; set; }
        public float CourseworkScore { get; set; }

        public float Total
        {
            get
            {
                var result = IntroScore + IntroHomeworkScore +
                             AdvanceScore + AdvanceHomeworkScore +
                             AsyncScore + AsyncHomeworkScore +
                             ArchitectureScore + ArchitectureHomeworkScore +
                             CordovaScore + CordovaHomeworkScore +
                             NativeScore + NativeHomeworkScore +
                             ReactScore + ReactHomeworkScore +
                             FirstExaminationScore + SecondExaminationScore;

                if (HasExaminationAnswer)
                {
                    result += 10;
                }

                if (HasCoursework)
                {
                    result += CourseworkScore;
                }

                return result;
            }
        }
    }
}