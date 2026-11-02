using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.AppSettings
{
    public class ScreenSettingService(ManagerDbContext dbContext) : IScreenSettingService
    {
        public Task<ScreenSetting?> RegistrationScreenSettingAsync =>
            dbContext
                .ScreenSettings
                .FirstOrDefaultAsync(setting => setting.Type == ScreenSettingConstants.RegistrationScreenType);

        public async Task<ScreenSetting?> UpdateRegistrationScreenSettingAsync(bool enabled)
        {
            var entity = await RegistrationScreenSettingAsync;
            if (entity == null)
            {
                return null;
            }
            entity.Enabled = enabled;
            dbContext.ScreenSettings.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}