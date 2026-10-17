using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Moq;
using StudentsManager.Mvc.Domain.Inputs.Surveys;
using Xunit;

namespace StudentsManager.Tests.Services.Surveys;

public class SurveySubmissionTests
{
    [Theory]
    [InlineData("picture.jpg", "image/jpeg", 1024 * 1024, true)]
    [InlineData("picture.jpeg", "image/jpeg", 5 * 1024 * 1024, true)]
    [InlineData("picture.PNG", "image/png", 1024 * 1024, true)]
    [InlineData("picture.webp", "image/webp", 1024 * 1024, true)]
    [InlineData("picture.jpg", "image/jpeg", 0, false)]
    [InlineData("picture.jpg", "image/jpeg", 6 * 1024 * 1024, false)]
    [InlineData("picture.gif", "image/gif", 1024 * 1024, false)]
    [InlineData("picture.svg", "image/svg+xml", 1024 * 1024, false)]
    [InlineData("picture.png", "text/html", 1024 * 1024, false)]
    [InlineData("picture.png", "image/jpeg", 1024 * 1024, false)]
    public void PictureMustMatchAllowedFormatAndSize(string fileName, string contentType, long length, bool expectedIsValid)
    {
        var picture = new Mock<IFormFile>();
        picture.SetupGet(file => file.FileName).Returns(fileName);
        picture.SetupGet(file => file.ContentType).Returns(contentType);
        picture.SetupGet(file => file.Length).Returns(length);

        var input = CreateSubmission();
        input.Picture = picture.Object;

        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(input, new ValidationContext(input), validationResults, true);

        Assert.Equal(expectedIsValid, isValid);
        if (!expectedIsValid)
        {
            Assert.Contains(validationResults, result => result.MemberNames.Contains(nameof(input.Picture)));
        }
    }

    [Fact]
    public void PictureIsRequired()
    {
        var input = CreateSubmission();
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(input, new ValidationContext(input), validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, result => result.MemberNames.Contains(nameof(input.Picture)));
    }

    private static SurveySubmission CreateSubmission()
    {
        return new SurveySubmission
        {
            PreviousExperience = "C#",
            CourseExpectations = "Learn to build apps",
            Hobby = "Painting",
            DreamProject = "Build a learning app"
        };
    }
}