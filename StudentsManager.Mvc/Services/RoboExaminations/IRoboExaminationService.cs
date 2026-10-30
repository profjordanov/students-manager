using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Domain.Views.RoboExaminations;

namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public enum RoboExaminationServiceStatus
    {
        Success,
        InvalidType,
        UserNotFound,
        NoActiveExamination,
        RoboQuizRequired,
        NoVariants,
        AlreadySubmitted,
        AttemptNotFound,
        InvalidAnswers,
        Duplicate,
        NotReevaluable,
        EvaluationSuperseded
    }

    public sealed record RoboExaminationServiceResult<T>(
        RoboExaminationServiceStatus Status,
        T? Value,
        string? Message = null);

    public interface IRoboExaminationService
    {
        Task<RoboExaminationServiceResult<RoboExaminationView>> GetOrAssignAsync(Guid userId);
        Task<RoboExaminationServiceResult<RoboExaminationSummaryView>> CreateAsync(RoboExaminationInput input);
        Task<RoboExaminationServiceResult<RoboExaminationSubmissionView>> SubmitAsync(
            RoboExaminationSubmissionInput input,
            CancellationToken requestAborted);
        Task<RoboExaminationServiceResult<RoboExaminationSubmissionView>> ReevaluateAsync(int attemptId);
        Task<RoboExaminationServiceResult<IReadOnlyList<RoboExaminationResultView>>> GetResultsAsync(Guid userId);
    }
}
