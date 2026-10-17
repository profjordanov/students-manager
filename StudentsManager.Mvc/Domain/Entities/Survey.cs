using File = StudentsManager.Mvc.Domain.Entities._Base.File;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class Survey : File
    {
        public int Id { get; set; }

        public string PreviousExperience { get; set; } = string.Empty;
        public string CourseExpectations { get; set; } = string.Empty;
        public string Hobby { get; set; } = string.Empty;
        public string DreamProject { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}