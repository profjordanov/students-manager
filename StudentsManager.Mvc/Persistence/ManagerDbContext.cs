using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Persistence
{
    public class ManagerDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ManagerDbContext(DbContextOptions<ManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<QuestionOption> QuestionOptions { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<UserTopicResult> UserTopicResults { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<CourseExamination> CourseExaminations { get; set; }

        public DbSet<CourseExaminationSetting> CourseExaminationSettings { get; set; }

        public DbSet<StudentCourseExamination> StudentCourseExaminations { get; set; }

        public DbSet<StudentCourseExaminationUpload> StudentExaminationUploads { get; set; }

        public DbSet<ScreenSetting> ScreenSettings { get; set; }

        public DbSet<ExaminationAnswer> ExaminationAnswers { get; set; }

        public DbSet<ForumQuestion> ForumQuestions { get; set; }

        public DbSet<ForumComment> ForumComments { get; set; }

        public DbSet<UserCoursework> UserCourseworks { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<PersonalityTestChoice> PersonalityTestChoices { get; set; }

        public DbSet<PersonalityTestResult> PersonalityTestResults { get; set; }

        public DbSet<PowerUpAnalysis> PowerUpAnalyses { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureQuestionOptionRelations();
            builder.ConfigureTestQuestionRelations();
            builder.ConfigureUserAnswerRelations();
            builder.ConfigureUserTopicResultRelations();
            builder.ConfigureHomeworkRelations();
            builder.ConfigureStudentCourseExaminationRelations();
            builder.ConfigureExaminationUploadRelations();
            builder.ConfigureExaminationAnswersRelations();
            builder.ConfigureCourseRelations();
            builder.ConfigureForum();
            builder.ConfigureUserCourseworks();
            builder.ConfigureSurveys();
            builder.ConfigurePersonalityTestChoices();
            builder.ConfigurePersonalityTestResults();
            builder.ConfigurePowerUpAnalyses();

            builder.ConfigureCourses();
            builder.ConfigureCourseExamination();

            base.OnModelCreating(builder);
        }
    }
}