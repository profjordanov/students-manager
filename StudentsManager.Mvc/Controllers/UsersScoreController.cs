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
            // Select only the name columns: loading the whole User would also read every Base64EncodePicture.
            var scores = await context.UsersScore
                .Include(score => score.User)
                .AsNoTracking()
                .Select(score => new
                {
                    Score = score,
                    score.User!.UserName,
                    score.User.FullName
                })
                .ToListAsync();

            // Total is [NotMapped], so it is computed in memory after materialization.
            var result = scores.Select(item => new UserScoreView
            {
                UserId = item.Score.UserId,
                UserName = item.UserName,
                FullName = item.FullName,
                FirstExaminationScore = item.Score.FirstExaminationScore,
                SecondExaminationScore = item.Score.SecondExaminationScore,
                Points = item.Score.Points,
                Total = item.Score.Total
            });

            return Ok(result);
        }
    }
}
