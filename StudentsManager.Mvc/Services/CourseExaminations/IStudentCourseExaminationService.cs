using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Services.CourseExaminations
{
    public interface IStudentCourseExaminationService
    {
        Task<StudentCourseExamination> GetOrCreateAsync(Guid userId, string examinationType);

        Task<StudentCourseExamination> SetScoreAsync(Guid userId, string examinationType, int score);

        Task AddToUserScoreAsync(Guid userId, string examinationType);
    }
}