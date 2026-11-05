using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using Xunit;

namespace StudentsManager.Tests.Services.UserGithubs;

public class UserGithubInputTests
{
    [Theory]
    [InlineData("https://github.com/octocat")]
    [InlineData("https://github.com/octocat/Hello-World")]
    [InlineData("https://github.com/octocat?tab=repositories")]
    [InlineData("http://github.com/octocat")]
    [InlineData("https://GitHub.com/octocat")]
    [InlineData("https://www.github.com/octocat")]
    [InlineData("https://gist.github.com/octocat/6cad326836d38bd3a7ae")]
    [InlineData("  https://github.com/octocat  ")]
    public void AcceptsGithubLinks(string link)
    {
        Assert.Empty(Validate(ValidInput(githubLink: link)));
    }

    [Theory]
    [InlineData("github.com/octocat")]
    [InlineData("octocat")]
    [InlineData("not a link")]
    [InlineData("javascript:alert(1)")]
    [InlineData("ftp://github.com/octocat")]
    [InlineData("https://example.com/octocat")]
    [InlineData("https://github.com.example.test/octocat")]
    [InlineData("https://notgithub.com/octocat")]
    [InlineData("https://github.com")]
    [InlineData("https://github.com/")]
    [InlineData("https://octocat@github.com/octocat")]
    [InlineData("https://github.com:8443/octocat")]
    [InlineData("https://github.com/octo cat")]
    public void RejectsLinksThatAreNotGithubLinks(string link)
    {
        var errors = Validate(ValidInput(githubLink: link));

        Assert.Contains(errors, result => result.MemberNames.Contains(nameof(UserGithubInput.GithubLink)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RequiresGithubLinkAndDescription(string value)
    {
        var errors = Validate(new UserGithubInput { GithubLink = value, Description = value });

        Assert.Contains(errors, result => result.MemberNames.Contains(nameof(UserGithubInput.GithubLink)));
        Assert.Contains(errors, result => result.MemberNames.Contains(nameof(UserGithubInput.Description)));
    }

    [Fact]
    public void AcceptsValuesAtTheLengthLimits()
    {
        const string prefix = "https://github.com/";
        var input = ValidInput(
            githubLink: prefix + new string('a', UserGithub.GithubLinkMaxLength - prefix.Length),
            description: new string('d', UserGithub.DescriptionMaxLength));

        Assert.Empty(Validate(input));
    }

    [Fact]
    public void RejectsValuesAboveTheLengthLimits()
    {
        const string prefix = "https://github.com/";
        var input = ValidInput(
            githubLink: prefix + new string('a', UserGithub.GithubLinkMaxLength - prefix.Length + 1),
            description: new string('d', UserGithub.DescriptionMaxLength + 1));

        var errors = Validate(input);

        Assert.Contains(errors, result => result.MemberNames.Contains(nameof(UserGithubInput.GithubLink)));
        Assert.Contains(errors, result => result.MemberNames.Contains(nameof(UserGithubInput.Description)));
    }

    private static UserGithubInput ValidInput(
        string githubLink = "https://github.com/octocat",
        string description = "My GitHub profile") =>
        new() { GithubLink = githubLink, Description = description };

    private static List<ValidationResult> Validate(UserGithubInput input)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(input, new ValidationContext(input), results, validateAllProperties: true);
        return results;
    }
}
