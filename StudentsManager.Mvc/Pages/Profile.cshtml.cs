using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Auth;

namespace StudentsManager.Mvc.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ManagerDbContext _dbContext;
        private readonly IPrincipalService _principalService;

        public ProfileModel(ManagerDbContext dbContext, IPrincipalService principalService)
        {
            _dbContext = dbContext;
            _principalService = principalService;
        }

        public User Student { get; private set; }

        public int StudentAnswersCount { get; private set; }
        public int StudentTopicResultsCount { get; private set; }
        public int StudentHomeworksCount { get; private set; }
        public int StudentCourseExaminationsCount { get; private set; }

        public int GetRandom()
        {
            var random = new Random();
            return random.Next(2, 100);
        }

        public async Task OnGetAsync()
        {
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);

            Student = await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            StudentAnswersCount = await _dbContext
                .UserAnswers
                .CountAsync(answer => answer.UserId == userId);

            StudentTopicResultsCount = await _dbContext
                .UserTopicResults
                .CountAsync(result => result.UserId == userId);

            StudentHomeworksCount = await _dbContext
                .Homeworks
                .CountAsync(homework => homework.UserId == userId);

            StudentCourseExaminationsCount = await _dbContext
                .StudentCourseExaminations
                .Include(examination => examination.Uploads)
                .CountAsync(examination =>
                    examination.UserId == userId && examination.Uploads.Any());
        }
    }
}