using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using StudentsManager.Mvc.Services.UserGithubs;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class UserGithubsController(IUserGithubsService service) : ControllerBase
    {
        public const string UserIdHeaderName = "X-User-Id";

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromHeader(Name = UserIdHeaderName)] string? userId)
        {
            if (!TryParseUserId(userId, out var parsedUserId))
                return InvalidUserId();

            var result = await service.GetAsync(parsedUserId);
            return Map(result, Ok);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromHeader(Name = UserIdHeaderName)] string? userId,
            [FromBody] UserGithubInput? input)
        {
            if (!TryParseUserId(userId, out var parsedUserId))
                return InvalidUserId();

            if (input == null || !TryValidateModel(input))
                return InvalidInput();

            var result = await service.CreateAsync(parsedUserId, input);
            return Map(result, view => CreatedAtAction(nameof(Get), view));
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromHeader(Name = UserIdHeaderName)] string? userId,
            [FromBody] UserGithubInput? input)
        {
            if (!TryParseUserId(userId, out var parsedUserId))
                return InvalidUserId();

            if (input == null || !TryValidateModel(input))
                return InvalidInput();

            var result = await service.UpdateAsync(parsedUserId, input);
            return Map(result, Ok);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromHeader(Name = UserIdHeaderName)] string? userId)
        {
            if (!TryParseUserId(userId, out var parsedUserId))
                return InvalidUserId();

            var status = await service.DeleteAsync(parsedUserId);
            return status == UserGithubsServiceStatus.Success ? NoContent() : Error(status);
        }

        private static bool TryParseUserId(string? header, out Guid userId) =>
            Guid.TryParse(header, out userId) && userId != Guid.Empty;

        private IActionResult InvalidUserId() =>
            BadRequest(new { message = $"A valid {UserIdHeaderName} header is required.", code = "invalidUserId" });

        private IActionResult InvalidInput()
        {
            var message = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error));
            return BadRequest(new { message = message ?? "Invalid request.", code = "validation" });
        }

        private IActionResult Map<T>(UserGithubsServiceResult<T> result, Func<T, IActionResult> onSuccess) =>
            result is { Status: UserGithubsServiceStatus.Success, Value: not null }
                ? onSuccess(result.Value)
                : Error(result.Status);

        private IActionResult Error(UserGithubsServiceStatus status)
        {
            var (statusCode, code, message) = status switch
            {
                UserGithubsServiceStatus.UserNotFound =>
                    (404, "userNotFound", "User not found."),
                UserGithubsServiceStatus.NotFound =>
                    (404, "githubNotFound", "The user has no GitHub link."),
                UserGithubsServiceStatus.AlreadyExists =>
                    (409, "alreadyExists", "The user already has a GitHub link. Use PUT to change it."),
                _ => throw new InvalidOperationException($"Unexpected UserGithubs status: {status}")
            };

            return StatusCode(statusCode, new { message, code });
        }
    }
}
