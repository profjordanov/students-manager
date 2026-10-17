using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.PersonalityTests;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.PersonalityTests;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class PersonalityTestController(IPersonalityTestsService personalityTestsService, IPrincipalService principalService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            var result = await personalityTestsService.GetResultAsync(userId);
            if (result != null)
            {
                ViewData["AlreadyCompleted"] = true;
                return View(nameof(Result), result);
            }

            return View(PersonalityTestConstants.Questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Choice([FromBody] PersonalityTestChoiceInput? input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            if (await personalityTestsService.HasCompletedAsync(userId))
            {
                return Conflict();
            }

            var recorded = await personalityTestsService.RecordChoiceAsync(userId, input.QuestionNumber, input.AnswerNumber);
            return recorded ? NoContent() : BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete()
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            if (await personalityTestsService.HasCompletedAsync(userId))
            {
                return Conflict();
            }

            var completed = await personalityTestsService.CompleteAsync(userId);
            return completed ? NoContent() : BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Feedback(PersonalityTestFeedbackInput? input)
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            if (input == null || !ModelState.IsValid)
            {
                TempData["FeedbackError"] = "Моля, избери оценка от 1 до 5.";
                return RedirectToAction(nameof(Result));
            }

            if (!await personalityTestsService.SaveFeedbackAsync(userId, input.Rating!.Value, input.Comment))
            {
                return BadRequest();
            }

            TempData["FeedbackSaved"] = true;
            return RedirectToAction(nameof(Result));
        }

        [HttpGet]
        public async Task<IActionResult> Result()
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            var result = await personalityTestsService.GetResultAsync(userId);
            return View(result);
        }
    }
}
