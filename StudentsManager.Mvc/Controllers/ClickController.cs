using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Click;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Clicks;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class ClickController(IClicksService service, IPrincipalService principalService) : Controller
    {
        // GET: Click
        public ActionResult Index()
        {
            return View();
        }

        //GET: Click/Details?type=professional
        public ActionResult Details([FromQuery] string type)
        {
            TempData["QuestionsContentType"] = type;
            var result = service.GetQuestionsContent(type);
            return Json(result);
        }

        // POST: Click/SaveResults
        [HttpPost]
        public async Task<IActionResult> SaveResults([FromForm] ClickResultsRootObject rootObject)
        {
            var questionsContentType = TempData["QuestionsContentType"].ToString() ?? throw new ArgumentNullException();
            await service.SaveAndPublishAnswersAsync(rootObject, principalService.GetUserIdByClaimsPrincipal(User), questionsContentType);
            return Ok(new SaveResultsModel(true));
        }
    }
}