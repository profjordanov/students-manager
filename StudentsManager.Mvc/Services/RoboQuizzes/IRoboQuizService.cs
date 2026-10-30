using StudentsManager.Mvc.Domain.Inputs.RoboQuizzes;
using StudentsManager.Mvc.Domain.Views.RoboQuizzes;

namespace StudentsManager.Mvc.Services.RoboQuizzes
{
    public enum RoboQuizServiceStatus
    {
        Success,
        InvalidType,
        UserNotFound,
        NoActiveExamination,
        NoVariants,
        Exhausted,
        QuizNotFound,
        Forbidden,
        NotAssigned,
        Duplicate
    }

    public sealed record RoboQuizServiceResult<T>(RoboQuizServiceStatus Status, T? Value);

    public interface IRoboQuizService
    {
        Task<RoboQuizServiceResult<RoboQuizView>> GetNextAsync(Guid userId);
        Task<RoboQuizServiceResult<RoboQuizSummaryView>> CreateAsync(RoboQuizInput input);
        Task<RoboQuizServiceResult<RoboQuizResultCreatedView>> SubmitResultAsync(RoboQuizResultInput input);
        Task<IReadOnlyList<RoboQuizResultView>> GetResultsAsync(Guid userId);
    }
}
