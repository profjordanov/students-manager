using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Services.Courses
{
    public class CourseSeeder
    {
        public static readonly Course HybridMobileAppCourse = new()
        {
            Id = Guid.Parse("788f26d7-b885-4568-99d5-ef83e4e88ef4"),
            Description = "Hybrid Mobile App"
        };

        public static readonly List<Course> Courses = new()
        {
            HybridMobileAppCourse
        };
    }
}