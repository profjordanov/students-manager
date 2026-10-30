namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public sealed class RoboExaminationEvaluationException : Exception
    {
        public RoboExaminationEvaluationException(string message, string? raw = null, Exception? innerException = null)
            : base(message, innerException)
        {
            Raw = raw;
        }

        public string? Raw { get; }
    }
}
