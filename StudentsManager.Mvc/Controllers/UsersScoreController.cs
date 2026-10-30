using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Views.UsersScore;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UsersScoreController(ManagerDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserScoreView>>> GetAll()
        {
            var scores = await context.UsersScore
                .AsNoTracking()
                .ToListAsync();

            // Total is [NotMapped], so it is computed in memory after materialization.
            var result = scores.Select(score => new UserScoreView
            {
                UserId = score.UserId,
                FirstExaminationScore = score.FirstExaminationScore,
                SecondExaminationScore = score.SecondExaminationScore,
                Points = score.Points,
                Total = score.Total
            });

            return Ok(result);
        }
    }
}
