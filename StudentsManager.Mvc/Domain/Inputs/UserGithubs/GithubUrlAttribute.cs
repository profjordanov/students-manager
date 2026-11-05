using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.UserGithubs
{
    // Accepts an absolute http(s) link to github.com (or one of its subdomains, e.g. gist.github.com)
    // that points at something below the site root. Null/blank values are left to [Required].
    public sealed class GithubUrlAttribute : ValidationAttribute
    {
        private const string GithubHost = "github.com";

        public GithubUrlAttribute()
            : base("The {0} field must be a link to a GitHub profile or repository, for example https://github.com/octocat.")
        {
        }

        public override bool IsValid(object? value) =>
            value switch
            {
                null => true,
                string text when string.IsNullOrWhiteSpace(text) => true,
                string text => IsGithubLink(text),
                _ => false
            };

        public static bool IsGithubLink(string text)
        {
            var link = text.Trim();
            if (link.Any(character => char.IsWhiteSpace(character) || char.IsControl(character)))
                return false;

            if (!Uri.TryCreate(link, UriKind.Absolute, out var uri))
                return false;

            return (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp)
                && uri.IsDefaultPort
                && string.IsNullOrEmpty(uri.UserInfo)
                && IsGithubHost(uri.IdnHost)
                && uri.AbsolutePath.Trim('/').Length > 0;
        }

        private static bool IsGithubHost(string host) =>
            host.Equals(GithubHost, StringComparison.OrdinalIgnoreCase)
            || host.EndsWith("." + GithubHost, StringComparison.OrdinalIgnoreCase);
    }
}
