using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Services.RoboExaminations;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class RoboExaminationController(IRoboExaminationService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? userId)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
                return BadRequest(new { message = "A valid userId is required.", code = "invalidUserId" });

            var result = await service.GetOrAssignAsync(parsedUserId);
            return RoboExaminationControllerHelpers.Map(this, result, Ok);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoboExaminationInput? input)
        {
            if (RoboExaminationControllerHelpers.IsInvalidInput(this, input))
                return RoboExaminationControllerHelpers.InvalidInput(this);

            var result = await service.CreateAsync(input!);
            return RoboExaminationControllerHelpers.Map(this, result, value => StatusCode(201, value));
        }
    }
}
