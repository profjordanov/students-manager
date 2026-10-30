using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.RoboQuizzes;
using StudentsManager.Mvc.Services.RoboQuizzes;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class RoboQuizController(IRoboQuizService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetNext([FromQuery] string? userId)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
                return BadRequest(new { message = "A valid userId is required." });

            var result = await service.GetNextAsync(parsedUserId);
            return result.Status switch
            {
                RoboQuizServiceStatus.Success => Ok(result.Value),
                RoboQuizServiceStatus.UserNotFound => NotFound(new { message = "User not found." }),
                RoboQuizServiceStatus.NoActiveExamination => StatusCode(403, new { message = "Няма активен изпит" }),
                RoboQuizServiceStatus.NoVariants => NotFound(new { message = "Няма налични тестове" }),
                RoboQuizServiceStatus.Exhausted => StatusCode(410, new { message = "Вече сте предали RoboQuiz за този изпит" }),
                _ => throw new InvalidOperationException($"Unexpected RoboQuiz status: {result.Status}")
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoboQuizInput input)
        {
            if (!TryValidateModel(input))
                return InvalidInput();

            var result = await service.CreateAsync(input);
            return result.Status switch
            {
                RoboQuizServiceStatus.Success => StatusCode(StatusCodes.Status201Created, result.Value),
                RoboQuizServiceStatus.InvalidType =>
                    BadRequest(new { message = "Type must match a configured examination setting." }),
                RoboQuizServiceStatus.Duplicate => Conflict(new { message = "A quiz variant with this type and name already exists." }),
                _ => throw new InvalidOperationException($"Unexpected RoboQuiz status: {result.Status}")
            };
        }

        private IActionResult InvalidInput()
        {
            var message = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error));

            return BadRequest(new { message = message ?? "Invalid request." });
        }
    }
}
