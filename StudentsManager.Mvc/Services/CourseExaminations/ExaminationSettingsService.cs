using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.Home;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.CourseExaminations
{
    public class ExaminationSettingsService(ManagerDbContext dbContext) : IExaminationSettingsService
    {
        public async Task<CourseExaminationSettingsView> GetViewAsync()
        {
            return new CourseExaminationSettingsView(await CanAccessTheFirstAsync(), await CanAccessTheSecondAsync());
        }

        public Task<CourseExaminationSetting?> GetFirstEnabledAsync()
        {
            return dbContext.CourseExaminationSettings
                .AsNoTracking()
                .Where(setting => setting.Enabled)
                .OrderBy(setting => setting.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CanAccessTheFirstAsync()
        {
            var entity = await FirstByTypeAsync(CourseExaminationConstants.FirstType);
            return entity is { Enabled: true };
        }

        public async Task<bool> CanAccessTheSecondAsync()
        {
            var entity = await FirstByTypeAsync(CourseExaminationConstants.SecondType);
            return entity is { Enabled: true };
        }

        public Task MakeFirstAvailableAsync()
        {
            return SetEnabledAsync(CourseExaminationConstants.FirstType, true);
        }

        public Task MakeSecondAvailableAsync()
        {
            return SetEnabledAsync(CourseExaminationConstants.SecondType, true);
        }

        public Task MakeFirstUnavailableAsync()
        {
            return SetEnabledAsync(CourseExaminationConstants.FirstType, false);
        }

        public Task MakeSecondUnavailableAsync()
        {
            return SetEnabledAsync(CourseExaminationConstants.SecondType, false);
        }

        private async Task SetEnabledAsync(string type, bool enabled)
        {
            var entity = await FirstByTypeAsync(type)
                ?? throw new InvalidOperationException($"Examination setting '{type}' is not configured.");

            await dbContext
                .CourseExaminationSettings
                .Where(setting => enabled || setting.Id == entity.Id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(setting => setting.Enabled, setting => enabled && setting.Id == entity.Id));
        }

        private Task<CourseExaminationSetting?> FirstByTypeAsync(string type)
        {
            return dbContext
                .CourseExaminationSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(setting => setting.Type == type);
        }
    }
}