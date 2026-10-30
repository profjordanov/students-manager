namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public interface IRoboExaminationEvaluator
    {
        Task<RoboExaminationEvaluation> EvaluateAsync(
            IReadOnlyList<RoboExaminationEvaluationItem> items,
            CancellationToken cancellationToken);
    }
}
