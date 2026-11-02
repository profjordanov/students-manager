using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Auth;
using StudentsManager.Mvc.Mappings;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Courses;
using StudentsManager.Mvc.Services.Storage;

namespace StudentsManager.Mvc.Services.Auth
{
    public class AuthService(
        UserManager<User?> userManager,
        SignInManager<User?> signInManager,
        ManagerDbContext dbContext,
        IStorageService storageService)
        : IAuthService
    {
        public async Task<User?> LoginAsync(Credentials model)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null) return null;

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect) return null;

            await signInManager.SignInAsync(user, false);

            return user;
        }

        public async Task<User?> RegisterAsync(Register model)
        {
            var user = model.ToEntity();
            var creationResult = await userManager.CreateAsync(user, model.Password);
            if (!creationResult.Succeeded) return null;

            var userCourse = new UserCourse
            {
                UserId = user.Id,
                CourseId = CourseSeeder.HybridMobileAppCourse.Id
            };

            await dbContext.UserCourses.AddAsync(userCourse);
            await dbContext.SaveChangesAsync();

            await storageService.CreateBlobContainerByNameAsync(user.Id.ToString());

            await signInManager.SignInAsync(user, false);

            return user;
        }

        public Task LogoutAsync()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<User?> ResetPasswordAsync(ResetPassword model)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null) return null;

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var identityResult = await userManager.ResetPasswordAsync(user, resetToken, model.Password);
            if (!identityResult.Succeeded) return null;

            await signInManager.SignInAsync(user, false);

            return user;
        }
    }
}