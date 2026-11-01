using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Services.AppSettings;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(IScreenSettingService settingService) : ControllerBase
    {
        [RequireAdminKey]
        [HttpGet("enable/reg")]
        public async Task<IActionResult> EnableRegistrationScreen()
        {
            await settingService.UpdateRegistrationScreenSettingAsync(true);
            return Ok();
        }

        [RequireAdminKey]
        [HttpGet("disable/reg")]
        public async Task<IActionResult> DisableRegistrationScreen()
        {
            await settingService.UpdateRegistrationScreenSettingAsync(false);
            return Ok();
        }
    }
}