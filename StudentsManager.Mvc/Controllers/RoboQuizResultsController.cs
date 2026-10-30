using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.RoboQuizzes;
using StudentsManager.Mvc.Services.RoboQuizzes;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class RoboQuizResultsController(IRoboQuizService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoboQuizResultInput? input)
        {
            if (input == null || !TryValidateModel(input))
            {
                var message = ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)
                    .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error));
                return BadRequest(new { message = message ?? "Invalid request." });
            }

            var result = await service.SubmitResultAsync(input);
            return result.Status switch
            {
                RoboQuizServiceStatus.Success => CreatedAtAction(
                    nameof(GetByUser),
                    new { userId = input.UserId },
                    result.Value),
                RoboQuizServiceStatus.UserNotFound or RoboQuizServiceStatus.QuizNotFound =>
                    NotFound(new { message = "User or quiz not found." }),
                RoboQuizServiceStatus.Forbidden =>
                    StatusCode(403, new { message = "The quiz is not part of the active examination." }),
                RoboQuizServiceStatus.NotAssigned =>
                    StatusCode(403, new { message = "This quiz variant is not assigned to you." }),
                RoboQuizServiceStatus.Duplicate =>
                    Conflict(new { message = "A quiz result for this examination type has already been submitted." }),
                _ => throw new InvalidOperationException($"Unexpected RoboQuiz result status: {result.Status}")
            };
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
                return BadRequest(new { message = "A valid userId is required." });

            return Ok(await service.GetResultsAsync(parsedUserId));
        }
    }
}
