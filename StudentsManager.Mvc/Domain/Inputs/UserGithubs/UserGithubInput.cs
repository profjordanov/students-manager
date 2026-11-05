using System.ComponentModel.DataAnnotations;
using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Domain.Inputs.UserGithubs
{
    public class UserGithubInput
    {
        [Required]
        [StringLength(UserGithub.GithubLinkMaxLength)]
        [GithubUrl]
        public string GithubLink { get; set; } = string.Empty;

        [Required]
        [StringLength(UserGithub.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;
    }
}
