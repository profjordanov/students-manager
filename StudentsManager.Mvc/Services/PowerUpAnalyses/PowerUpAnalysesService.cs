using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.PowerUpAnalyses;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.PersonalityTests;
using StudentsManager.Mvc.Services.Surveys;

namespace StudentsManager.Mvc.Services.PowerUpAnalyses
{
    public class PowerUpAnalysesService : IPowerUpAnalysesService
    {
        private readonly ManagerDbContext _dbContext;
        private readonly ISurveysService _surveysService;
        private readonly IPersonalityTestsService _personalityTestsService;

        public PowerUpAnalysesService(
            ManagerDbContext dbContext,
            ISurveysService surveysService,
            IPersonalityTestsService personalityTestsService)
        {
            _dbContext = dbContext;
            _surveysService = surveysService;
            _personalityTestsService = personalityTestsService;
        }

        public Task<PowerUpAnalysisView?> GetByUserIdAsync(Guid userId)
        {
            return _dbContext.PowerUpAnalyses
                .AsNoTracking()
                .Where(analysis => analysis.UserId == userId)
                .Select(analysis => new PowerUpAnalysisView
                {
                    UserId = analysis.UserId,
                    AnalysisResult = analysis.AnalysisResult
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<PowerUpAnalysisOverviewView>> GetOverviewAsync()
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .Where(user => !user.IsDeleted)
                .Where(user => user.Surveys!.Any() || user.PersonalityTestResults!.Any() || user.PowerUpAnalyses!.Any())
                .OrderBy(user => user.FullName)
                .Select(user => new { user.Id, user.FullName })
                .ToListAsync();

            var overview = new List<PowerUpAnalysisOverviewView>(users.Count);
            foreach (var user in users)
            {
                overview.Add(new PowerUpAnalysisOverviewView
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Survey = await _surveysService.GetByUserIdAsync(user.Id),
                    Personality = await _personalityTestsService.GetResultAsync(user.Id),
                    Analysis = await GetByUserIdAsync(user.Id)
                });
            }

            return overview;
        }

        public async Task<PowerUpAnalysisView?> GenerateAsync(Guid userId)
        {
            var survey = await _surveysService.GetByUserIdAsync(userId);
            var personality = await _personalityTestsService.GetResultAsync(userId);
            if (survey == null || personality == null)
            {
                return null;
            }

            var result = Compose(survey.FullName, survey, personality);

            var analysis = await _dbContext.PowerUpAnalyses.SingleOrDefaultAsync(a => a.UserId == userId);
            if (analysis == null)
            {
                analysis = new PowerUpAnalysis { UserId = userId };
                await _dbContext.PowerUpAnalyses.AddAsync(analysis);
            }

            analysis.AnalysisResult = result;
            await _dbContext.SaveChangesAsync();

            return new PowerUpAnalysisView { UserId = userId, AnalysisResult = result };
        }

        private static string Compose(
            string fullName,
            Domain.Views.Surveys.SurveyResultView survey,
            Domain.Views.PersonalityTests.PersonalityTestResultView personality)
        {
            var strongest = personality.Scores.Where(score => score.IsStrongest).Select(score => score.Trait.ToString()).ToList();
            var breakdown = string.Join(", ", personality.Scores.Select(score => $"{score.Trait} {score.Percentage:0.##}%"));

            var text = new StringBuilder();
            text.AppendLine($"Power Up Analysis for {fullName}");
            text.AppendLine($"Personality: strongest trait(s) - {string.Join(" and ", strongest)} ({breakdown}).");
            text.AppendLine($"Experience: {survey.PreviousExperience}");
            text.AppendLine($"Goals for the course: {survey.CourseExpectations}");
            text.AppendLine($"Interests: {survey.Hobby}");
            text.AppendLine($"Dream project: {survey.DreamProject}");
            return text.ToString().TrimEnd();
        }
    }
}
