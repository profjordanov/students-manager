using Microsoft.AspNetCore.Identity;
using StudentsManager.Mvc.Domain.Entities._Base;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class User : IdentityUser<Guid>, IDeletableEntity, IAuditInfo
    {
        public string FullName { get; set; }
        public string FacultyNumber { get; set; }
        public string? Base64EncodePicture { get; set; }

        public ICollection<UserAnswer>? UserAnswers { get; set; }
        public ICollection<UserTopicResult>? UserTopicResults { get; set; }
        public ICollection<Homework>? Homeworks { get; set; }
        public ICollection<StudentCourseExamination>? StudentExaminations { get; set; }
        public ICollection<ExaminationAnswer>? ExaminationAnswers { get; set; }
        public IEnumerable<UserCourse>? UserCourses { get; set; }
        public ICollection<ForumQuestion>? ForumQuestions { get; set; }
        public ICollection<ForumComment>? ForumComments { get; set; }

        public ICollection<UserCoursework>? Courseworks { get; set; }
        public ICollection<Survey>? Surveys { get; set; }
        public ICollection<PersonalityTestChoice>? PersonalityTestChoices { get; set; }
        public ICollection<PersonalityTestResult>? PersonalityTestResults { get; set; }
        public ICollection<PowerUpAnalysis>? PowerUpAnalyses { get; set; }
        public ICollection<RoboQuizUserResult>? RoboQuizResults { get; set; }
        public ICollection<RoboExaminationAttempt>? RoboExaminationAttempts { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}