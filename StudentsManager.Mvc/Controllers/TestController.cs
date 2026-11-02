using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Options;
using StudentsManager.Mvc.Services.Tests;
using StudentsManager.Mvc.Services.Topics;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class TestController(
        ITopicsService topicsService,
        ITestsService testsService,
        IPrincipalService principalService,
        IUserAnswersService userAnswersService,
        IUserTopicResultsService userTopicResultsService)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            var result = await topicsService.GetUserTopicResultsAsync(userId);
            return View(result);
        }

        [HttpGet]
        [Route("start/{topicId}")]
        public async Task<IActionResult> Start([FromRoute] Guid topicId)
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            if (await topicsService.HasUserTopicResultAsync(userId, topicId))
                return RedirectToAction("Result", new { topicId });
            var result = await testsService.GetTestAsync(topicId, userId);

            TempData["topicId"] = topicId;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAnswer([FromBody] ChosenOption optionModel)
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            await userAnswersService.AddAsync(userId, optionModel.ChosenOptionId);
            return Ok();
        }

        [HttpPost]
        [Route("start/SubmitAnswers")]
        public async Task<IActionResult> SubmitAnswers([FromQuery] string v, [FromBody] TestBindingModel model)
        {
            var topicId = TempData["topicId"].ToString() ?? throw new ArgumentNullException();
            var userId = principalService.GetUserIdByClaimsPrincipal(User);

            await userTopicResultsService.ProcessAsync(Guid.Parse(topicId), userId);

            var result = new
            {
                redirect = $"{Url.Action("Done", "Test")}"
            };

            return Json(result);
        }

        [HttpGet]
        public IActionResult Done()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Result(Guid topicId)
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            var result = await userTopicResultsService.GetSingleAsync(topicId, userId);
            return View(result);
        }
    }
}