using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.Statistics;
using StudentsManager.Mvc.Persistence;
using static StudentsManager.Mvc.Services.CourseExaminations.CourseExaminationConstants;
using static StudentsManager.Mvc.Services.Topics.TopicsSeeder;

namespace StudentsManager.Mvc.Services.Statistics
{
    public class StudentsGradingServiceV2 : IStudentsGradingService
    {
        private readonly ManagerDbContext _dbContext;

        public StudentsGradingServiceV2(ManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<StudentGrade>> GetAsync()
        {
            var students = await _dbContext
                .Users
                .AsNoTracking()
                .Where(user => user.IsDeleted == false)
                .Include(user => user.ExaminationAnswers)
                .Include(user => user.Courseworks)
                .ToListAsync();

            var userTopicResults = await _dbContext
                .UserTopicResults
                .AsNoTracking()
                .ToListAsync();

            var homeworks = await _dbContext
                .Homeworks
                .AsNoTracking()
                .ToListAsync();

            var studentCourseExaminations = await _dbContext
                .StudentCourseExaminations
                .AsNoTracking()
                .Include(examination => examination.CourseExamination)
                .ToListAsync();

            return students
                .Select(user => ToView(user, userTopicResults, homeworks, studentCourseExaminations))
                .ToList();
        }

        private static StudentGrade ToView(
            User student,
            IReadOnlyCollection<UserTopicResult> userTopicResults,
            IReadOnlyCollection<Homework> homeworks,
            IReadOnlyCollection<StudentCourseExamination> studentCourseExaminations)
        {
            var hasCoursework = student.Courseworks != null && student.Courseworks.Any(coursework => coursework.Points > 0);
            var courseworkScore = hasCoursework ? (float)student.Courseworks!.First(coursework => coursework.Points > 0).Points : 0;

            return new StudentGrade
            {
                StudentId = student.Id,
                StudentName = student.FullName,
                StudentFacultyNumber = student.FacultyNumber,

                IntroScore = Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, IntroTopic.Id)),
                IntroHomework = StudentHomeworkByTopicId(homeworks, student.Id, IntroTopic.Id),

                AdvanceScore =
                    Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, AdvanceTopic.Id)),
                AdvanceHomework = StudentHomeworkByTopicId(homeworks, student.Id, AdvanceTopic.Id),

                AsyncScore =
                    Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id,
                        AsyncProgrammingTopic.Id)),
                AsyncHomework = StudentHomeworkByTopicId(homeworks, student.Id, AsyncProgrammingTopic.Id),

                ArchitectureScore =
                    Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, JqueryTopic.Id)),
                ArchitectureHomework = StudentHomeworkByTopicId(homeworks, student.Id, JqueryTopic.Id),

                CordovaScore =
                    Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, CordovaIntroTopic.Id)),
                CordovaHomework = StudentHomeworkByTopicId(homeworks, student.Id, CordovaIntroTopic.Id),

                NativeScore = Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, OnsenTopic.Id)),
                NativeHomework = StudentHomeworkByTopicId(homeworks, student.Id, OnsenTopic.Id),

                ReactScore = Set10DividedSafeScore(StudentScoreByTopicId(userTopicResults, student.Id, ReactTopic.Id)),
                ReactHomework = StudentHomeworkByTopicId(homeworks, student.Id, ReactTopic.Id),

                FirstExaminationScore =
                    SetSafeScore(StudentExaminationScoreByType(studentCourseExaminations, student.Id, FirstType)),
                SecondExaminationScore =
                    SetSafeScore(StudentExaminationScoreByType(studentCourseExaminations, student.Id, SecondType)),

                HasExaminationAnswer = student.ExaminationAnswers != null && student.ExaminationAnswers.Count != 0,

                HasCoursework = hasCoursework,
                CourseworkScore = courseworkScore
            };
        }

        private static int? StudentScoreByTopicId(
            IEnumerable<UserTopicResult> userTopicResults,
            Guid studentId,
            Guid topicId)
        {
            return userTopicResults
                .FirstOrDefault(x => x.TopicId == topicId && x.UserId == studentId)?
                .Score;
        }

        private static string? StudentHomeworkByTopicId(
            IEnumerable<Homework> homeworks,
            Guid studentId,
            Guid topicId)
        {
            return homeworks
                .FirstOrDefault(x => x.TopicId == topicId && x.UserId == studentId)?
                .Link;
        }

        private static int? StudentExaminationScoreByType(
            IEnumerable<StudentCourseExamination> examinations,
            Guid studentId,
            string type)
        {
            return examinations
                .FirstOrDefault(x => x.CourseExamination!.Type == type && x.UserId == studentId)?
                .Score;
        }

        private static float Set10DividedSafeScore(int? nullableScore)
        {
            return nullableScore == null ? 0f : (float)(nullableScore / 10.0);
        }

        private static float SetSafeScore(int? nullableScore)
        {
            return nullableScore == null ? 0f : (float)nullableScore;
        }
    }
}