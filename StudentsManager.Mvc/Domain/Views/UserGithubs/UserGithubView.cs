namespace StudentsManager.Mvc.Domain.Views.UserGithubs
{
    public class UserGithubView
    {
        public Guid UserId { get; set; }
        public string GithubLink { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
