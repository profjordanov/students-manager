using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Surveys;

namespace StudentsManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExaminationSettingsService _settingsService;
        private readonly IPrincipalService _principalService;
        private readonly ISurveysService _surveysService;

        public HomeController(
            IExaminationSettingsService settingsService,
            IPrincipalService principalService,
            ISurveysService surveysService)
        {
            _settingsService = settingsService;
            _principalService = principalService;
            _surveysService = surveysService;
        }

        public async Task<IActionResult> Index()
        {
            var examinationSettingsView = await _settingsService.GetViewAsync();
            var showSurvey = false;

            if (_principalService.IsSignedInByClaimsPrincipal(User))
            {
                var userId = _principalService.GetUserIdByClaimsPrincipal(User);
                showSurvey = await _surveysService.GetByUserIdAsync(userId) is null;
            }

            ViewData["ShowSurvey"] = showSurvey;
            return View(examinationSettingsView);
        }
    }
}