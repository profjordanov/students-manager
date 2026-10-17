using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Persistence
{
    public static class OnModelCreatingConfiguration
    {
        public static void ConfigureTestQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<TestQuestion>()
                .HasIndex(question => question.TopicId);

            builder
                .Entity<TestQuestion>()
                .HasOne(question => question.Topic)
                .WithMany(topic => topic.TestQuestions)
                .HasForeignKey(question => question.TopicId);
        }

        public static void ConfigureQuestionOptionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<QuestionOption>()
                .HasOne(option => option.TestQuestion)
                .WithMany(question => question.Options)
                .HasForeignKey(question => question.TestQuestionId);

            builder
                .Entity<TestQuestion>()
                .Property(option => option.Description)
                .IsRequired()
                .HasMaxLength(900)
                .HasDefaultValue("")
                .HasComment("Test Question description");

            builder
                .Entity<QuestionOption>()
                .Property(option => option.Description)
                .IsRequired()
                .HasMaxLength(900)
                .HasDefaultValue("")
                .HasComment("Question Option description");

            builder
                .Entity<QuestionOption>()
                .Property(option => option.ImageAddress)
                .IsRequired(false)
                .HasMaxLength(900)
                .HasComment("Question Option image address");
        }

        public static void ConfigureUserAnswerRelations(this ModelBuilder builder)
        {
            builder
                .Entity<UserAnswer>()
                .HasIndex(answer => answer.QuestionOptionId);

            builder
                .Entity<UserAnswer>()
                .HasIndex(answer => answer.UserId);

            builder
                .Entity<UserAnswer>()
                .HasOne(answer => answer.QuestionOption)
                .WithMany(option => option.UserAnswers)
                .HasForeignKey(answer => answer.QuestionOptionId);

            builder
                .Entity<UserAnswer>()
                .HasOne(answer => answer.User)
                .WithMany(user => user.UserAnswers)
                .HasForeignKey(answer => answer.UserId);
        }

        public static void ConfigureUserTopicResultRelations(this ModelBuilder builder)
        {
            builder
                .Entity<UserTopicResult>()
                .HasOne(ut => ut.Topic)
                .WithMany(topic => topic.UserTopics)
                .HasForeignKey(ut => ut.TopicId);

            builder
                .Entity<UserTopicResult>()
                .HasOne(ut => ut.User)
                .WithMany(user => user.UserTopicResults)
                .HasForeignKey(ut => ut.UserId);
        }

        public static void ConfigureHomeworkRelations(this ModelBuilder builder)
        {
            builder
                .Entity<Homework>()
                .HasOne(h => h.Topic)
                .WithMany(topic => topic.Homeworks)
                .HasForeignKey(h => h.TopicId);

            builder
                .Entity<Homework>()
                .HasOne(h => h.User)
                .WithMany(user => user.Homeworks)
                .HasForeignKey(h => h.UserId);

            builder
                .Entity<Homework>()
                .Property(h => h.RepositoryLink)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasComment("Link to the repository of the homework");

            builder
                .Entity<Homework>()
                .Property(h => h.FileName)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasComment("Name of the file with the homework");

            builder
                .Entity<Homework>()
                .Property(h => h.Path)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasComment("Path to the file with the homework");

            builder
                .Entity<Homework>()
                .Property(h => h.Extension)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasComment("Extension of the file with the homework");
        }

        public static void ConfigureStudentCourseExaminationRelations(this ModelBuilder builder)
        {
            builder
                .Entity<StudentCourseExamination>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Entity<StudentCourseExamination>()
                .HasOne(sce => sce.User)
                .WithMany(user => user.StudentExaminations)
                .HasForeignKey(sce => sce.UserId);

            builder
                .Entity<StudentCourseExamination>()
                .HasOne(sce => sce.CourseExamination)
                .WithMany(ce => ce.StudentExaminations)
                .HasForeignKey(sce => sce.CourseExaminationId);
        }

        public static void ConfigureExaminationUploadRelations(this ModelBuilder builder)
        {
            builder
                .Entity<StudentCourseExaminationUpload>()
                .HasOne(sceu => sceu.StudentCourseExamination)
                .WithMany(sce => sce.Uploads)
                .HasForeignKey(sceu => sceu.StudentCourseExaminationId);
        }

        public static void ConfigureExaminationAnswersRelations(this ModelBuilder builder)
        {
            builder
                .Entity<ExaminationAnswer>()
                .HasOne(ea => ea.User)
                .WithMany(u => u.ExaminationAnswers)
                .HasForeignKey(ea => ea.UserId);

            builder
                .Entity<ExaminationAnswer>()
                .Property(ea => ea.Result)
                .IsRequired();

            builder
                .Entity<ExaminationAnswer>()
                .Property(ea => ea.ContentType)
                .IsRequired()
                .HasMaxLength(150);
        }

        public static void ConfigureCourseRelations(this ModelBuilder builder)
        {
            builder
                .Entity<Topic>()
                .HasOne(topic => topic.Course)
                .WithMany(course => course.Topics)
                .HasForeignKey(topic => topic.CourseId)
                .IsRequired(false);

            builder
                .Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(course => course.UserCourses)
                .HasForeignKey(topic => topic.CourseId);

            builder
                .Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(user => user.UserCourses)
                .HasForeignKey(uc => uc.UserId);
        }

        public static void ConfigureForum(this ModelBuilder builder)
        {
            builder
                .Entity<ForumQuestion>()
                .HasOne(q => q.User)
                .WithMany(user => user.ForumQuestions)
                .HasForeignKey(q => q.UserId);

            builder
                .Entity<ForumComment>()
                .HasOne(c => c.User)
                .WithMany(user => user.ForumComments)
                .HasForeignKey(c => c.UserId);

            builder
                .Entity<ForumComment>()
                .HasOne(c => c.ForumQuestion)
                .WithMany(user => user.Comments)
                .HasForeignKey(c => c.ForumQuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ForumQuestion>()
                .Property(fc => fc.Description)
                .IsRequired()
                .HasMaxLength(1500)
                .IsUnicode()
                .HasDefaultValue("")
                .HasComment("Forum Question description");

            builder
                .Entity<ForumComment>()
                .Property(fc => fc.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode()
                .HasDefaultValue("")
                .HasComment("Forum Comment description");
        }

        public static void ConfigureUserCourseworks(this ModelBuilder builder)
        {
            builder
                .Entity<UserCoursework>()
                .Property(uc => uc.Link)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("Link to the coursework");

            builder
                .Entity<UserCoursework>()
                .HasIndex(coursework => coursework.UserId)
                .IsUnique();

            builder
                .Entity<UserCoursework>()
                .HasOne(uc => uc.User)
                .WithMany(user => user.Courseworks)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();
        }

        public static void ConfigureSurveys(this ModelBuilder builder)
        {
            builder
                .Entity<Survey>()
                .HasIndex(survey => survey.UserId)
                .IsUnique();

            builder
                .Entity<Survey>()
                .HasOne(survey => survey.User)
                .WithMany(user => user.Surveys)
                .HasForeignKey(survey => survey.UserId)
                .IsRequired();

            builder
                .Entity<Survey>()
                .Property(survey => survey.PreviousExperience)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode()
                .HasComment("Previous experience with programming");

            builder
                .Entity<Survey>()
                .Property(survey => survey.CourseExpectations)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode()
                .HasComment("Expectations for the course");

            builder
                .Entity<Survey>()
                .Property(survey => survey.Hobby)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode()
                .HasComment("Hobbies and interests");

            builder
                .Entity<Survey>()
                .Property(survey => survey.DreamProject)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode()
                .HasComment("Dream project");

            builder
                .Entity<Survey>()
                .Property(survey => survey.FileName)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Entity<Survey>()
                .Property(survey => survey.Path)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Entity<Survey>()
                .Property(survey => survey.Extension)
                .IsRequired()
                .HasMaxLength(100);
        }

        public static void ConfigurePersonalityTestChoices(this ModelBuilder builder)
        {
            builder
                .Entity<PersonalityTestChoice>()
                .HasOne(choice => choice.User)
                .WithMany(user => user.PersonalityTestChoices)
                .HasForeignKey(choice => choice.UserId)
                .IsRequired();

            builder
                .Entity<PersonalityTestChoice>()
                .Property(choice => choice.QuestionNumber)
                .HasComment("Personality test question number");

            builder
                .Entity<PersonalityTestChoice>()
                .Property(choice => choice.AnswerNumber)
                .HasComment("Chosen statement of the question (1 or 2)");

            builder
                .Entity<PersonalityTestChoice>()
                .Property(choice => choice.Trait)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasComment("Personality trait of the chosen statement");
        }

        public static void ConfigurePowerUpAnalyses(this ModelBuilder builder)
        {
            builder
                .Entity<PowerUpAnalysis>()
                .HasIndex(analysis => analysis.UserId)
                .IsUnique();

            builder
                .Entity<PowerUpAnalysis>()
                .HasOne(analysis => analysis.User)
                .WithMany(user => user.PowerUpAnalyses)
                .HasForeignKey(analysis => analysis.UserId)
                .IsRequired();

            builder
                .Entity<PowerUpAnalysis>()
                .Property(analysis => analysis.AnalysisResult)
                .IsRequired()
                .IsUnicode()
                .HasComment("Analysis based on the user's survey and personality test");
        }

        public static void ConfigurePersonalityTestResults(this ModelBuilder builder)
        {
            builder
                .Entity<PersonalityTestResult>()
                .HasIndex(result => result.UserId)
                .IsUnique();

            builder
                .Entity<PersonalityTestResult>()
                .HasOne(result => result.User)
                .WithMany(user => user.PersonalityTestResults)
                .HasForeignKey(result => result.UserId)
                .IsRequired();

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.ActionScore)
                .HasComment("Number of chosen Action statements");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.ProcessScore)
                .HasComment("Number of chosen Process statements");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.PeopleScore)
                .HasComment("Number of chosen People statements");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.IdeaScore)
                .HasComment("Number of chosen Idea statements");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.ActionPercentage)
                .HasPrecision(5, 2)
                .HasComment("Action score as a percentage of the maximum Action score");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.ProcessPercentage)
                .HasPrecision(5, 2)
                .HasComment("Process score as a percentage of the maximum Process score");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.PeoplePercentage)
                .HasPrecision(5, 2)
                .HasComment("People score as a percentage of the maximum People score");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.IdeaPercentage)
                .HasPrecision(5, 2)
                .HasComment("Idea score as a percentage of the maximum Idea score");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.CompletedAtUtc)
                .HasComment("When the user completed the test");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.FeedbackRating)
                .HasComment("How accurately the result matches the user, from 1 to 5");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.FeedbackComment)
                .HasMaxLength(1000)
                .HasComment("Optional free-text feedback about the result");

            builder
                .Entity<PersonalityTestResult>()
                .Property(result => result.FeedbackSubmittedAtUtc)
                .HasComment("When the user last submitted feedback");
        }

        public static void ConfigureCourses(this ModelBuilder builder)
        {
            builder
                .Entity<Course>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode()
                .HasDefaultValue("")
                .HasComment("Course description");
        }

        public static void ConfigureCourseExamination(this ModelBuilder builder)
        {
            builder
                .Entity<CourseExamination>()
                .Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasComment("Course examination type");
            
            builder
                .Entity<CourseExamination>()
                .Property(c => c.ResourceUrl)
                .IsRequired()
                .HasMaxLength(500)
                .HasDefaultValue("")
                .HasComment("Course examination resource url");

            builder
                .Entity<CourseExamination>()
                .Property(c => c.ExtraResourceUrl)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasComment("Course examination extra resource url");
        }        
    }
}