using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using StudentsManager.Mvc.Settings;

namespace StudentsManager.Mvc.Controllers._Base
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class RequireAdminKeyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var adminKey = context.HttpContext.RequestServices.GetRequiredService<IOptions<AdminExaminationSettings>>().Value.AdminKey;

            if (string.IsNullOrWhiteSpace(adminKey)
                || !context.HttpContext.Request.Headers.TryGetValue("X-Admin-Key", out var providedKey)
                || providedKey.Count != 1
                || providedKey[0] != adminKey)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
