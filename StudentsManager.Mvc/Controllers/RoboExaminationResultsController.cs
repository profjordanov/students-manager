using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Services.RoboExaminations;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class RoboExaminationResultsController(IRoboExaminationService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoboExaminationSubmissionInput? input)
        {
            if (RoboExaminationControllerHelpers.IsInvalidInput(this, input))
                return RoboExaminationControllerHelpers.InvalidInput(this);

            var result = await service.SubmitAsync(input!);
            return RoboExaminationControllerHelpers.Map(
                this,
                result,
                value => CreatedAtAction(nameof(GetByUser), new { userId = input!.UserId }, value),
                alreadySubmittedStatus: 409);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
                return BadRequest(new { message = "A valid userId is required.", code = "invalidUserId" });

            var result = await service.GetResultsAsync(parsedUserId);
            return RoboExaminationControllerHelpers.Map(this, result, Ok);
        }

        [HttpPost("{attemptId:int}/evaluate")]
        public async Task<IActionResult> Reevaluate(int attemptId)
        {
            var result = await service.ReevaluateAsync(attemptId);
            return RoboExaminationControllerHelpers.Map(this, result, Ok);
        }
    }
}
