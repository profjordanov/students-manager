namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserGithub
    {
        public const int GithubLinkMaxLength = 500;
        public const int DescriptionMaxLength = 1000;

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string GithubLink { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
