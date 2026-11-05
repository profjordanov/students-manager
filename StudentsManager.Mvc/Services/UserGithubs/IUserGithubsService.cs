using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using StudentsManager.Mvc.Domain.Views.UserGithubs;

namespace StudentsManager.Mvc.Services.UserGithubs
{
    public enum UserGithubsServiceStatus
    {
        Success,
        UserNotFound,
        NotFound,
        AlreadyExists
    }

    public sealed record UserGithubsServiceResult<T>(UserGithubsServiceStatus Status, T? Value);

    public interface IUserGithubsService
    {
        Task<List<UserGithubView>> GetAllAsync();
        Task<UserGithubsServiceResult<UserGithubView>> GetAsync(Guid userId);
        Task<UserGithubsServiceResult<UserGithubView>> CreateAsync(Guid userId, UserGithubInput input);
        Task<UserGithubsServiceResult<UserGithubView>> UpdateAsync(Guid userId, UserGithubInput input);
        Task<UserGithubsServiceStatus> DeleteAsync(Guid userId);
    }
}
