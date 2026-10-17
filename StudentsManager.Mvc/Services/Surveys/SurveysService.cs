using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Surveys;
using StudentsManager.Mvc.Domain.Views.Surveys;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Storage;

namespace StudentsManager.Mvc.Services.Surveys
{
    public class SurveysService : ISurveysService
    {
        private static readonly Expression<Func<Survey, SurveyResultView>> ResultProjection = survey => new SurveyResultView
        {
            FullName = survey.User!.FullName,
            PreviousExperience = survey.PreviousExperience,
            CourseExpectations = survey.CourseExpectations,
            Hobby = survey.Hobby,
            DreamProject = survey.DreamProject,
            PicturePath = survey.Path!,
            CreatedAtUtc = survey.CreatedAtUtc
        };

        private readonly ManagerDbContext _dbContext;
        private readonly IStorageService _storageService;

        public SurveysService(ManagerDbContext dbContext, IStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public Task<SurveyResultView?> GetByUserIdAsync(Guid userId)
        {
            return _dbContext.Surveys
                .AsNoTracking()
                .Where(survey => survey.UserId == userId)
                .Select(ResultProjection)
                .SingleOrDefaultAsync();
        }

        public async Task SubmitAsync(Guid userId, SurveySubmission input)
        {
            var picture = input.Picture ?? throw new ArgumentException("A picture is required.", nameof(input));
            var extension = Path.GetExtension(picture.FileName).ToLowerInvariant();
            var fileName = $"{SurveyConstants.BlobNamePrefix}{Guid.NewGuid():N}{extension}";

            using var stream = picture.OpenReadStream();
            await _storageService.UploadToContainerAsync(userId.ToString(), stream, fileName);

            var survey = new Survey
            {
                UserId = userId,
                PreviousExperience = input.PreviousExperience!.Trim(),
                CourseExpectations = input.CourseExpectations!.Trim(),
                Hobby = input.Hobby!.Trim(),
                DreamProject = input.DreamProject!.Trim(),
                FileName = fileName,
                Path = $"{_storageService.PathBasis}{userId}/{fileName}",
                Extension = picture.ContentType,
                CreatedAtUtc = DateTimeOffset.UtcNow
            };

            await _dbContext.Surveys.AddAsync(survey);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<SurveyResultView>> GetAllAsync()
        {
            return await _dbContext.Surveys
                .AsNoTracking()
                .Where(survey => !survey.User!.IsDeleted)
                .OrderByDescending(survey => survey.CreatedAtUtc)
                .Select(ResultProjection)
                .ToListAsync();
        }
    }
}