using StudentsManager.Mvc.Domain.Views.PersonalityTests;

namespace StudentsManager.Mvc.Services.PersonalityTests
{
    public interface IPersonalityTestsService
    {
        Task<bool> HasCompletedAsync(Guid userId);

        Task<bool> RecordChoiceAsync(Guid userId, int questionNumber, int answerNumber);

        Task<bool> CompleteAsync(Guid userId);

        Task<PersonalityTestResultView?> GetResultAsync(Guid userId);

        Task<bool> SaveFeedbackAsync(Guid userId, int rating, string? comment);
    }
}
