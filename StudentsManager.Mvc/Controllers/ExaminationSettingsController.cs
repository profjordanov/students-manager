using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.CourseExaminations;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationSettingsController(IExaminationSettingsService service) : ControllerBase
    {
        [HttpGet("enable/first")]
        public async Task<IActionResult> EnableFirst()
        {
            await service.MakeFirstAvailableAsync();
            return Ok("First test enabled!");
        }

        [HttpGet("disable/first")]
        public async Task<IActionResult> DisableFirst()
        {
            await service.MakeFirstUnavailableAsync();
            return Ok("First test disabled!");
        }

        [HttpGet("enable/second")]
        public async Task<IActionResult> EnableSecond()
        {
            await service.MakeSecondAvailableAsync();
            return Ok("Second tests enabled!");
        }

        [HttpGet("disable/second")]
        public async Task<IActionResult> DisableSecond()
        {
            await service.MakeSecondUnavailableAsync();
            return Ok("Second tests disabled!");
        }

        [HttpGet("enabled")]
        public async Task<IActionResult> GetEnabled()
        {
            var setting = await service.GetFirstEnabledAsync();
            return setting == null
                ? NoContent()
                : Ok(new { id = setting.Id, type = setting.Type });
        }
    }
}