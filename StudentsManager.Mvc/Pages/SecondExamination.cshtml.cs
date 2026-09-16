using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Pages._Base;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.CourseExaminations;
using static StudentsManager.Mvc.Services.CourseExaminations.CourseExaminationConstants;

namespace StudentsManager.Mvc.Pages
{
    [Authorize]
    public class SecondExaminationModel : ExaminationModel
    {
        public SecondExaminationModel(
            IPrincipalService principalService,
            IExaminationSettingsService settingsService,
            IStudentCourseExaminationService studentExaminationService,
            IExaminationUploadService uploadService) :
            base(principalService, settingsService, studentExaminationService, uploadService)
        {
        }

        public async Task OnGetAsync()
        {
            await GetAsync(SecondType);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await PostAsync(SecondType);
            return RedirectToPage();
        }
    }
}