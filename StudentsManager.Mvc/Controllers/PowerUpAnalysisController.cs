using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Services.PowerUpAnalyses;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class PowerUpAnalysisController(IPowerUpAnalysesService service) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await service.GetOverviewAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateFromView([FromForm] Guid userId)
        {
            await service.GenerateAsync(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
