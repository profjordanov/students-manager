using StudentsManager.Mvc.Domain.Views.PowerUpAnalyses;

namespace StudentsManager.Mvc.Services.PowerUpAnalyses
{
    public interface IPowerUpAnalysesService
    {
        Task<PowerUpAnalysisView?> GetByUserIdAsync(Guid userId);

        Task<IReadOnlyList<PowerUpAnalysisOverviewView>> GetOverviewAsync();

        /// <summary>Builds and stores the analysis; returns null when the survey or personality test is missing.</summary>
        Task<PowerUpAnalysisView?> GenerateAsync(Guid userId);
    }
}
