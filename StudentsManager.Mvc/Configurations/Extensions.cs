using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.Students;
using System.Text;

namespace StudentsManager.Mvc.Configurations;

public static class Extensions
{
    public static StudentView? ToView(this User? user)
    {
        if (user == null)
        {
            return null;
        }
        return new StudentView(user.Id, user.UserName, user.Email.Encode(), user.FullName, user.FacultyNumber, user.Base64EncodePicture);
    }

    public static string? Encode(this string? value)
    {
        return string.IsNullOrEmpty(value) ? value : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
}