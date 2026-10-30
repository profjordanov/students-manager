using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Click;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Clicks;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class ClickController : Controller
    {
        private readonly IPrincipalService _principalService;
        private readonly IClicksService _service;

        public ClickController(IClicksService service, IPrincipalService principalService)
        {
            _service = service;
            _principalService = principalService;
        }

        // GET: Click
        public ActionResult Index()
        {
            return View();
        }

        //GET: Click/Details?type=professional
        public ActionResult Details([FromQuery] string type)
        {
            TempData["QuestionsContentType"] = type;
            var result = _service.GetQuestionsContent(type);
            return Json(result);
        }

        // POST: Click/SaveResults
        [HttpPost]
        public async Task<IActionResult> SaveResults([FromForm] ClickResultsRootObject rootObject)
        {
            var questionsContentType = TempData["QuestionsContentType"].ToString() ?? throw new ArgumentNullException();
            await _service.SaveAndPublishAnswersAsync(rootObject, _principalService.GetUserIdByClaimsPrincipal(User), questionsContentType);
            return Ok(new SaveResultsModel(true));
        }
    }
}