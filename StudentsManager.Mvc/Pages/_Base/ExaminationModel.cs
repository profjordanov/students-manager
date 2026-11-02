using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.CourseExaminations;
using static StudentsManager.Mvc.Services.CourseExaminations.CourseExaminationConstants;

namespace StudentsManager.Mvc.Pages._Base
{
    public abstract class ExaminationModel : PageModel, IExaminationModel
    {
        protected readonly IPrincipalService PrincipalService;
        protected readonly IExaminationSettingsService SettingsService;
        protected readonly IStudentCourseExaminationService StudentExaminationService;
        protected readonly IExaminationUploadService UploadService;

        protected ExaminationModel(
            IPrincipalService principalService,
            IExaminationSettingsService settingsService,
            IStudentCourseExaminationService studentExaminationService,
            IExaminationUploadService uploadService)
        {
            PrincipalService = principalService;
            SettingsService = settingsService;
            StudentExaminationService = studentExaminationService;
            UploadService = uploadService;
        }

        public bool Enabled { get; protected set; }
        public Guid StudentCourseExaminationId { get; protected set; }
        public string ExaminationResourceUrl { get; protected set; }
        public string? ExaminationExtraResourceUrl { get; protected set; }
        public List<StudentCourseExaminationUpload> Uploads { get; protected set; }
        public bool FilesLimitReached { get; protected set; }

        [BindProperty] public Guid StudentExaminationId { get; set; }

        [BindProperty]
        public IFormFile? File { get; set; }

        protected async Task GetAsync(string examinationType)
        {
            if (examinationType == FirstType)
                Enabled = await SettingsService.CanAccessTheFirstAsync();
            else
                Enabled = await SettingsService.CanAccessTheSecondAsync();
            if (Enabled)
            {
                var userId = PrincipalService.GetUserIdByClaimsPrincipal(User);
                var studentExamination = await StudentExaminationService.GetOrCreateAsync(userId, examinationType);
                StudentCourseExaminationId = studentExamination.Id;
                ExaminationResourceUrl = studentExamination.CourseExamination.ResourceUrl;
                ExaminationExtraResourceUrl = studentExamination.CourseExamination.ExtraResourceUrl;
                Uploads = await UploadService.GetAsync(StudentCourseExaminationId);
                FilesLimitReached = Uploads.Count >= 4;
            }
        }

        protected async Task<StudentCourseExaminationUpload> PostAsync(string examinationType)
        {
            var userId = PrincipalService.GetUserIdByClaimsPrincipal(User);
            var result = await UploadService.AttachFileAsync(StudentExaminationId, examinationType, File, userId);
            await StudentExaminationService.AddToUserScoreAsync(userId, examinationType);
            return result;
        }
    }
}