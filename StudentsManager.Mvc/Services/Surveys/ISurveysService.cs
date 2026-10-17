using StudentsManager.Mvc.Domain.Inputs.Surveys;
using StudentsManager.Mvc.Domain.Views.Surveys;

namespace StudentsManager.Mvc.Services.Surveys
{
    public interface ISurveysService
    {
        Task<SurveyResultView?> GetByUserIdAsync(Guid userId);

        Task SubmitAsync(Guid userId, SurveySubmission input);

        Task<IReadOnlyList<SurveyResultView>> GetAllAsync();
    }
}