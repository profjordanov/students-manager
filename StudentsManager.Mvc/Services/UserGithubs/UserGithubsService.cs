using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using StudentsManager.Mvc.Domain.Views.UserGithubs;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Services.UserGithubs
{
    public class UserGithubsService(ManagerDbContext dbContext) : IUserGithubsService
    {
        public Task<List<UserGithubView>> GetAllAsync()
        {
            return dbContext.UserGithubs
                .AsNoTracking()
                .Select(github => new UserGithubView
                {
                    UserId = github.UserId,
                    GithubLink = github.GithubLink,
                    Description = github.Description
                })
                .ToListAsync();
        }

        public async Task<UserGithubsServiceResult<UserGithubView>> GetAsync(Guid userId)
        {
            if (!await UserExistsAsync(userId))
                return new(UserGithubsServiceStatus.UserNotFound, null);

            var view = await dbContext.UserGithubs
                .AsNoTracking()
                .Where(github => github.UserId == userId)
                .Select(github => new UserGithubView
                {
                    UserId = github.UserId,
                    GithubLink = github.GithubLink,
                    Description = github.Description
                })
                .SingleOrDefaultAsync();

            return view == null
                ? new(UserGithubsServiceStatus.NotFound, null)
                : new(UserGithubsServiceStatus.Success, view);
        }

        public async Task<UserGithubsServiceResult<UserGithubView>> CreateAsync(Guid userId, UserGithubInput input)
        {
            if (!await UserExistsAsync(userId))
                return new(UserGithubsServiceStatus.UserNotFound, null);

            var github = new UserGithub
            {
                UserId = userId,
                GithubLink = input.GithubLink.Trim(),
                Description = input.Description.Trim()
            };

            dbContext.UserGithubs.Add(github);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (exception.IsUniqueConstraintViolation())
            {
                dbContext.Entry(github).State = EntityState.Detached;
                return new(UserGithubsServiceStatus.AlreadyExists, null);
            }

            return new(UserGithubsServiceStatus.Success, ToView(github));
        }

        public async Task<UserGithubsServiceResult<UserGithubView>> UpdateAsync(Guid userId, UserGithubInput input)
        {
            if (!await UserExistsAsync(userId))
                return new(UserGithubsServiceStatus.UserNotFound, null);

            var githubLink = input.GithubLink.Trim();
            var description = input.Description.Trim();

            var updatedRows = await dbContext.UserGithubs
                .Where(github => github.UserId == userId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(github => github.GithubLink, githubLink)
                    .SetProperty(github => github.Description, description));

            return updatedRows == 0
                ? new(UserGithubsServiceStatus.NotFound, null)
                : new(
                    UserGithubsServiceStatus.Success,
                    new UserGithubView { UserId = userId, GithubLink = githubLink, Description = description });
        }

        public async Task<UserGithubsServiceStatus> DeleteAsync(Guid userId)
        {
            if (!await UserExistsAsync(userId))
                return UserGithubsServiceStatus.UserNotFound;

            var deletedRows = await dbContext.UserGithubs
                .Where(github => github.UserId == userId)
                .ExecuteDeleteAsync();

            return deletedRows == 0
                ? UserGithubsServiceStatus.NotFound
                : UserGithubsServiceStatus.Success;
        }

        private Task<bool> UserExistsAsync(Guid userId) =>
            dbContext.Users.AnyAsync(user => user.Id == userId && !user.IsDeleted);

        private static UserGithubView ToView(UserGithub github) =>
            new()
            {
                UserId = github.UserId,
                GithubLink = github.GithubLink,
                Description = github.Description
            };
    }
}
