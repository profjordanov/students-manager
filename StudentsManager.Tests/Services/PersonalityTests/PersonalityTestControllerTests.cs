using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Inputs.PersonalityTests;
using StudentsManager.Mvc.Domain.Views.PersonalityTests;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.PersonalityTests;
using Xunit;

namespace StudentsManager.Tests.Services.PersonalityTests;

public class PersonalityTestControllerTests
{
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Mock<IPersonalityTestsService> _personalityTestsService = new();
    private readonly PersonalityTestController _controller;

    public PersonalityTestControllerTests()
    {
        var principalService = new Mock<IPrincipalService>();
        principalService.Setup(service => service.GetUserIdByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()))
            .Returns(_userId);

        _controller = new PersonalityTestController(_personalityTestsService.Object, principalService.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };
    }

    [Fact]
    public async Task IndexShowsAllQuestionsBeforeTheTestIsCompleted()
    {
        _personalityTestsService.Setup(service => service.GetResultAsync(_userId))
            .ReturnsAsync((PersonalityTestResultView)null);

        var result = Assert.IsType<ViewResult>(await _controller.Index());

        Assert.Same(PersonalityTestConstants.Questions, result.Model);
    }

    [Fact]
    public async Task IndexShowsTheSavedResultOnceTheTestIsCompleted()
    {
        var view = new PersonalityTestResultView();
        _personalityTestsService.Setup(service => service.GetResultAsync(_userId)).ReturnsAsync(view);

        var result = Assert.IsType<ViewResult>(await _controller.Index());

        Assert.Equal(nameof(PersonalityTestController.Result), result.ViewName);
        Assert.Same(view, result.Model);
        Assert.Equal(true, result.ViewData["AlreadyCompleted"]);
    }

    [Fact]
    public async Task ChoiceRecordsTheChoiceOfTheSignedInUser()
    {
        _personalityTestsService.Setup(service => service.RecordChoiceAsync(_userId, 3, 2)).ReturnsAsync(true);

        var result = await _controller.Choice(new PersonalityTestChoiceInput { QuestionNumber = 3, AnswerNumber = 2 });

        Assert.IsType<NoContentResult>(result);
        _personalityTestsService.Verify(service => service.RecordChoiceAsync(_userId, 3, 2), Times.Once);
    }

    [Fact]
    public async Task ChoiceRejectsAnUnknownAnswer()
    {
        _personalityTestsService.Setup(service => service.RecordChoiceAsync(_userId, 41, 1)).ReturnsAsync(false);

        var result = await _controller.Choice(new PersonalityTestChoiceInput { QuestionNumber = 41, AnswerNumber = 1 });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task ChoiceRejectsAMissingBody()
    {
        var result = await _controller.Choice(null);

        Assert.IsType<BadRequestResult>(result);
        _personalityTestsService.Verify(
            service => service.RecordChoiceAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()),
            Times.Never);
    }

    [Fact]
    public async Task ChoiceIsRejectedOnceTheTestIsCompleted()
    {
        _personalityTestsService.Setup(service => service.HasCompletedAsync(_userId)).ReturnsAsync(true);

        var result = await _controller.Choice(new PersonalityTestChoiceInput { QuestionNumber = 3, AnswerNumber = 2 });

        Assert.IsType<ConflictResult>(result);
        _personalityTestsService.Verify(
            service => service.RecordChoiceAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()),
            Times.Never);
    }

    [Fact]
    public async Task CompleteSavesTheResultOfTheSignedInUser()
    {
        _personalityTestsService.Setup(service => service.CompleteAsync(_userId)).ReturnsAsync(true);

        var result = await _controller.Complete();

        Assert.IsType<NoContentResult>(result);
        _personalityTestsService.Verify(service => service.CompleteAsync(_userId), Times.Once);
    }

    [Fact]
    public async Task CompleteRejectsAnUnfinishedTest()
    {
        _personalityTestsService.Setup(service => service.CompleteAsync(_userId)).ReturnsAsync(false);

        var result = await _controller.Complete();

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task CompleteIsRejectedOnceTheTestIsCompleted()
    {
        _personalityTestsService.Setup(service => service.HasCompletedAsync(_userId)).ReturnsAsync(true);

        var result = await _controller.Complete();

        Assert.IsType<ConflictResult>(result);
        _personalityTestsService.Verify(service => service.CompleteAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task ResultShowsTheResultOfTheSignedInUser()
    {
        var view = new PersonalityTestResultView();
        _personalityTestsService.Setup(service => service.GetResultAsync(_userId)).ReturnsAsync(view);

        var result = Assert.IsType<ViewResult>(await _controller.Result());

        Assert.Same(view, result.Model);
    }

    [Fact]
    public async Task ResultShowsNoResultBeforeTheTestIsCompleted()
    {
        _personalityTestsService.Setup(service => service.GetResultAsync(_userId))
            .ReturnsAsync((PersonalityTestResultView)null);

        var result = Assert.IsType<ViewResult>(await _controller.Result());

        Assert.Null(result.Model);
    }

    [Fact]
    public async Task FeedbackSavesTheFeedbackOfTheSignedInUser()
    {
        _controller.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
            new DefaultHttpContext(), Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
        _personalityTestsService.Setup(service => service.SaveFeedbackAsync(_userId, 4, "Accurate")).ReturnsAsync(true);

        var result = await _controller.Feedback(new PersonalityTestFeedbackInput { Rating = 4, Comment = "Accurate" });

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(PersonalityTestController.Result), redirect.ActionName);
        _personalityTestsService.Verify(service => service.SaveFeedbackAsync(_userId, 4, "Accurate"), Times.Once);
    }

    [Fact]
    public async Task FeedbackIsRejectedWithoutACompletedTest()
    {
        _personalityTestsService.Setup(service => service.SaveFeedbackAsync(_userId, 4, null)).ReturnsAsync(false);

        var result = await _controller.Feedback(new PersonalityTestFeedbackInput { Rating = 4 });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task FeedbackWithAnInvalidModelIsNotSaved()
    {
        _controller.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
            new DefaultHttpContext(), Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
        _controller.ModelState.AddModelError("Rating", "Required");

        var result = await _controller.Feedback(new PersonalityTestFeedbackInput());

        Assert.IsType<RedirectToActionResult>(result);
        _personalityTestsService.Verify(
            service => service.SaveFeedbackAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ControllerRequiresSignIn()
    {
        Assert.True(Attribute.IsDefined(typeof(PersonalityTestController), typeof(AuthorizeAttribute)));
    }

    [Theory]
    [InlineData(nameof(PersonalityTestController.Choice))]
    [InlineData(nameof(PersonalityTestController.Complete))]
    [InlineData(nameof(PersonalityTestController.Feedback))]
    public void PostActionsRequireAnAntiforgeryToken(string actionName)
    {
        var action = typeof(PersonalityTestController).GetMethod(actionName);

        Assert.True(Attribute.IsDefined(action, typeof(ValidateAntiForgeryTokenAttribute)));
    }
}
