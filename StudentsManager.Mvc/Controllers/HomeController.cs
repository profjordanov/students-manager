using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Surveys;

namespace StudentsManager.Mvc.Controllers
{
    public class HomeController(
        IExaminationSettingsService settingsService,
        IPrincipalService principalService,
        ISurveysService surveysService)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var examinationSettingsView = await settingsService.GetViewAsync();
            var showSurvey = false;

            if (principalService.IsSignedInByClaimsPrincipal(User))
            {
                var userId = principalService.GetUserIdByClaimsPrincipal(User);
                showSurvey = await surveysService.GetByUserIdAsync(userId) is null;
            }

            ViewData["ShowSurvey"] = showSurvey;
            return View(examinationSettingsView);
        }
    }
}