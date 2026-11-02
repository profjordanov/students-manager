using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.CourseExaminations
{
    public class StudentCourseExaminationService(ManagerDbContext dbContext) : IStudentCourseExaminationService
    {
        public async Task<StudentCourseExamination> GetOrCreateAsync(Guid userId, string examinationType)
        {
            var result = await dbContext
                .StudentCourseExaminations
                .Include(examination => examination.CourseExamination)
                .FirstOrDefaultAsync(examination =>
                    examination.UserId == userId && examination.CourseExamination.Type == examinationType);

            if (result == null) return await AssignedAsync(userId, examinationType);

            return result;
        }

        public async Task<StudentCourseExamination> SetScoreAsync(Guid userId, string examinationType, int score)
        {
            var studentExamination = await GetOrCreateAsync(userId, examinationType);
            studentExamination.Score = score;

            dbContext.StudentCourseExaminations.Update(studentExamination);
            await dbContext.SaveChangesAsync();

            return studentExamination;
        }

        public async Task<StudentCourseExamination> AssignedAsync(Guid userId, string examinationType)
        {
            var examinations = await dbContext
                .CourseExaminations
                .Where(examination => examination.Type == examinationType)
                .ToListAsync();

            var random = new Random();
            var position = random.Next(0, examinations.Count);

            var entity = new StudentCourseExamination
            {
                UserId = userId,
                CourseExaminationId = examinations[position].Id
            };

            await dbContext.StudentCourseExaminations.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task AddToUserScoreAsync(Guid userId, string examinationType)
        {
            var userScore = await dbContext.UsersScore.FirstOrDefaultAsync(score => score.UserId == userId);
            if (userScore == null)
            {
                return;
            }

            switch (examinationType)
            {
                case CourseExaminationConstants.FirstType:
                    userScore.FirstExaminationScore += 3;
                    break;
                case CourseExaminationConstants.SecondType:
                    userScore.SecondExaminationScore += 3;
                    break;
            }
            await dbContext.SaveChangesAsync();
        }
    }
}