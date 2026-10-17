using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Inputs.Surveys;
using StudentsManager.Mvc.Domain.Views.Surveys;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Surveys;
using Xunit;

namespace StudentsManager.Tests.Services.Surveys;

public class SurveyControllerTests
{
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Mock<ISurveysService> _surveysService = new();
    private readonly SurveyController _controller;

    public SurveyControllerTests()
    {
        var principalService = new Mock<IPrincipalService>();
        principalService.Setup(service => service.GetUserIdByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()))
            .Returns(_userId);

        _controller = new SurveyController(_surveysService.Object, principalService.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };
    }

    [Fact]
    public async Task IndexShowsFormBeforeSubmission()
    {
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync((SurveyResultView)null);

        var result = Assert.IsType<ViewResult>(await _controller.Index());

        Assert.IsType<SurveySubmission>(result.Model);
    }

    [Fact]
    public async Task IndexShowsSavedSubmission()
    {
        var submission = new SurveyResultView { FullName = "Student" };
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId)).ReturnsAsync(submission);

        var result = Assert.IsType<ViewResult>(await _controller.Index());

        Assert.Equal("Submitted", result.ViewName);
        Assert.Same(submission, result.Model);
    }

    [Fact]
    public async Task PostDoesNotSaveASecondSubmission()
    {
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync(new SurveyResultView());

        var result = Assert.IsType<RedirectToActionResult>(await _controller.Index(new SurveySubmission()));

        Assert.Equal("Index", result.ActionName);
        _surveysService.Verify(service => service.SubmitAsync(It.IsAny<Guid>(), It.IsAny<SurveySubmission>()), Times.Never);
    }

    [Fact]
    public async Task PostShowsInvalidFormWithoutSaving()
    {
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync((SurveyResultView)null);
        _controller.ModelState.AddModelError(nameof(SurveySubmission.Picture), "A picture is required.");
        var input = new SurveySubmission();

        var result = Assert.IsType<ViewResult>(await _controller.Index(input));

        Assert.Same(input, result.Model);
        _surveysService.Verify(service => service.SubmitAsync(It.IsAny<Guid>(), It.IsAny<SurveySubmission>()), Times.Never);
    }

    [Fact]
    public async Task PostSavesAndRedirects()
    {
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync((SurveyResultView)null);
        var input = new SurveySubmission();
        _surveysService.Setup(service => service.SubmitAsync(_userId, input)).Returns(Task.CompletedTask);

        var result = Assert.IsType<RedirectToActionResult>(await _controller.Index(input));

        Assert.Equal("Index", result.ActionName);
        _surveysService.Verify(service => service.SubmitAsync(_userId, input), Times.Once);
    }

    [Fact]
    public async Task ResultsShowsAllSubmissions()
    {
        IReadOnlyList<SurveyResultView> submissions = new[] { new SurveyResultView() };
        _surveysService.Setup(service => service.GetAllAsync()).ReturnsAsync(submissions);

        var result = Assert.IsType<ViewResult>(await _controller.Results());

        Assert.Same(submissions, result.Model);
    }

    [Fact]
    public void ControllerRequiresSignIn()
    {
        Assert.True(Attribute.IsDefined(typeof(SurveyController), typeof(AuthorizeAttribute)));
    }
}