namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public sealed record RoboExaminationEvaluation(int Grade, string Feedback, string Raw);

    public sealed record RoboExaminationEvaluationItem(
        string Question,
        string Answer);
}
