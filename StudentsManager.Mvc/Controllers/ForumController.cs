using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Forum;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Forum;

namespace StudentsManager.Mvc.Controllers
{
    public class ForumController(IForumService service, IPrincipalService principalService) : Controller
    {
        // GET: /Forum
        public async Task<IActionResult> Index([FromQuery] int pageNumber = 1)
        {
            var result = await service.GetPaginatedListAsync(pageNumber);
            return View(result);
        }

        // POST: /Forum
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            var tryQuestionValue = collection.TryGetValue("forumQuestion", out var questionValue);
            if (!tryQuestionValue || string.IsNullOrEmpty(questionValue)) return RedirectToAction(nameof(Index));

            await service.SaveQuestionAsync(
                questionValue.ToString(),
                principalService.GetUserIdByClaimsPrincipal(User));

            return RedirectToAction(nameof(Index));
        }

        // POST: Forum/Comment
        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> PostComment([FromBody] Comment input)
        {
            var result = await service.SaveCommentAsync(input, principalService.GetUserIdByClaimsPrincipal(User));
            return Json(result);
        }
    }
}