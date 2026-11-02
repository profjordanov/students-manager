using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.Homeworks;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController(IHomeworksService homeworksService) : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var result = await homeworksService.GetByUserAsync(userId);
            return Ok(result);
        }
    }
}