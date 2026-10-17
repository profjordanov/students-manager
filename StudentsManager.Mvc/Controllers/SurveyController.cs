using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.Surveys;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Surveys;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class SurveyController(ISurveysService surveysService, IPrincipalService principalService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            var result = await surveysService.GetByUserIdAsync(userId);
            return result == null ? View(new SurveySubmission()) : View("Submitted", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SurveySubmission input)
        {
            var userId = principalService.GetUserIdByClaimsPrincipal(User);
            if (await surveysService.GetByUserIdAsync(userId) != null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await surveysService.SubmitAsync(userId, input);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Results()
        {
            var results = await surveysService.GetAllAsync();
            return View(results);
        }
    }
}