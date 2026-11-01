using StudentsManager.Mvc.Domain.Views.RoboExaminations;

namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public interface IRoboExaminationEvaluator
    {
        Task<RoboExaminationEvaluation> EvaluateAsync(IReadOnlyList<RoboExaminationAnswerView> answers);
    }
}
