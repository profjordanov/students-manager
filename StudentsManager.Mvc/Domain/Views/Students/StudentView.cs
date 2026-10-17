namespace StudentsManager.Mvc.Domain.Views.Students
{
    public readonly record struct StudentView(Guid Id, string? UserName, string? Email, string FullName, string FacultyNumber, string? Base64EncodePicture);
}