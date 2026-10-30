using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.AppSettings;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Courses;
using StudentsManager.Mvc.Services.Options;
using StudentsManager.Mvc.Services.RoboExaminations;
using StudentsManager.Mvc.Services.Questions;
using StudentsManager.Mvc.Services.Topics;

namespace StudentsManager.Mvc.Services.Tests
{
    public class DatabaseSeeder
    {
        private readonly ManagerDbContext _dbContext;

        public DatabaseSeeder(ManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            RoboExaminationSeeder.UpgradeLegacyQuestions(_dbContext);

            var hasAnyCourses = _dbContext.Courses.Any();
            if (!hasAnyCourses)
            {
                _dbContext.Courses.AddRange(CourseSeeder.Courses);
                _dbContext.SaveChanges();
                var users = _dbContext.Users.Select(user => user.Id).ToList();
                if (users.Any())
                {
                    var userCourses = users.Select(userId => new UserCourse
                    {
                        UserId = userId,
                        CourseId = CourseSeeder.HybridMobileAppCourse.Id
                    });
                    _dbContext.UserCourses.AddRange(userCourses);
                    _dbContext.SaveChanges();
                }
            }

            if (_dbContext.Topics.Any()) return;

            _dbContext.Topics.AddRange(TopicsSeeder.Topics);
            _dbContext.SaveChanges();

            _dbContext.ScreenSettings.AddRange(ScreenSettingSeeder.ScreenSettings);
            _dbContext.SaveChanges();

            _dbContext.CourseExaminationSettings.AddRange(ExaminationSeeder.Settings);
            _dbContext.SaveChanges();

            _dbContext.CourseExaminations.AddRange(ExaminationSeeder.CourseExaminations);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(TestQuestionSeeder.TestQuestions);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(AsyncProgrammingTopicSeeder.AsyncQuestions);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(JqueryTopicSeeder.JqueryQuestions);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(CordovaTopicSeeder.CordovaIntroQuestions);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(OnsenTopicSeeder.OnsenQuestions);
            _dbContext.SaveChanges();

            _dbContext.TestQuestions.AddRange(ReactTopicSeeder.TestQuestions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(QuestionOptionSeeder.QuestionOptions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(AsyncProgrammingTopicSeeder.AsyncQuestionOptions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(JqueryTopicSeeder.JqueryQuestionOptions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(CordovaTopicSeeder.CordovaIntroQuestionOptions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(OnsenTopicSeeder.OnsenQuestionOptions);
            _dbContext.SaveChanges();

            _dbContext.QuestionOptions.AddRange(ReactTopicSeeder.QuestionOptions);
            _dbContext.SaveChanges();
        }

        private void SeedRoboExaminations()
        {
            RoboExaminationSeeder.UpgradeLegacyQuestions(_dbContext);

            if (_dbContext.RoboExaminations.Any(item => item.Type == "First" && item.Variant == "A"))
                return;

            var examination = RoboExaminationSeeder.FirstVariantA;
            _dbContext.RoboExaminations.Add(examination);
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                _dbContext.Entry(examination).State = EntityState.Detached;
            }
        }
    }
}
