using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using StudentsManager.Mvc.Domain.Views.UserGithubs;
using StudentsManager.Mvc.Services.UserGithubs;
using Xunit;

namespace StudentsManager.Tests.Services.UserGithubs;

public class UserGithubsControllerTests
{
    private static readonly Guid UserId = Guid.NewGuid();

    private static readonly string[] Actions =
    [
        nameof(UserGithubsController.Get),
        nameof(UserGithubsController.Create),
        nameof(UserGithubsController.Update),
        nameof(UserGithubsController.Delete)
    ];

    public static TheoryData<string, string> InvalidUserIdHeaders()
    {
        var data = new TheoryData<string, string>();
        foreach (var action in Actions)
        {
            foreach (var header in new[] { null, "", "   ", "not-a-guid", Guid.Empty.ToString() })
                data.Add(action, header);
        }

        return data;
    }

    [Theory]
    [InlineData(nameof(UserGithubsController.Get))]
    [InlineData(nameof(UserGithubsController.Create))]
    [InlineData(nameof(UserGithubsController.Update))]
    [InlineData(nameof(UserGithubsController.Delete))]
    public void ActionsReadTheUserIdFromTheXUserIdHeader(string action)
    {
        var userIdParameter = typeof(UserGithubsController).GetMethod(action)!.GetParameters()[0];

        var binding = Assert.Single(userIdParameter.GetCustomAttributes<FromHeaderAttribute>());

        Assert.Equal("X-User-Id", binding.Name);
    }

    [Theory]
    [MemberData(nameof(InvalidUserIdHeaders))]
    public async Task ActionsRejectMissingOrInvalidUserIdHeader(string action, string header)
    {
        var service = new Mock<IUserGithubsService>(MockBehavior.Strict);
        var controller = CreateController(service.Object);

        var response = Assert.IsType<BadRequestObjectResult>(await Invoke(controller, action, header, ValidInput()));

        Assert.Equal("invalidUserId", Property(response.Value, "code"));
        service.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(nameof(UserGithubsController.Get), UserGithubsServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(nameof(UserGithubsController.Get), UserGithubsServiceStatus.NotFound, 404, "githubNotFound")]
    [InlineData(nameof(UserGithubsController.Create), UserGithubsServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(nameof(UserGithubsController.Create), UserGithubsServiceStatus.AlreadyExists, 409, "alreadyExists")]
    [InlineData(nameof(UserGithubsController.Update), UserGithubsServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(nameof(UserGithubsController.Update), UserGithubsServiceStatus.NotFound, 404, "githubNotFound")]
    [InlineData(nameof(UserGithubsController.Delete), UserGithubsServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(nameof(UserGithubsController.Delete), UserGithubsServiceStatus.NotFound, 404, "githubNotFound")]
    public async Task ActionsMapServiceStatuses(
        string action,
        UserGithubsServiceStatus status,
        int expectedStatus,
        string expectedCode)
    {
        var failure = new UserGithubsServiceResult<UserGithubView>(status, null);
        var service = new Mock<IUserGithubsService>();
        service.Setup(item => item.GetAsync(It.IsAny<Guid>())).ReturnsAsync(failure);
        service.Setup(item => item.CreateAsync(It.IsAny<Guid>(), It.IsAny<UserGithubInput>())).ReturnsAsync(failure);
        service.Setup(item => item.UpdateAsync(It.IsAny<Guid>(), It.IsAny<UserGithubInput>())).ReturnsAsync(failure);
        service.Setup(item => item.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(status);
        var controller = CreateController(service.Object);

        var response = Assert.IsType<ObjectResult>(
            await Invoke(controller, action, UserId.ToString(), ValidInput()));

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedCode, Property(response.Value, "code"));
    }

    [Fact]
    public async Task GetReturnsTheLinkOfTheUserFromTheHeader()
    {
        var view = ViewOf(ValidInput());
        var service = new Mock<IUserGithubsService>();
        service.Setup(item => item.GetAsync(UserId)).ReturnsAsync(Success(view));

        var response = Assert.IsType<OkObjectResult>(await CreateController(service.Object).Get(UserId.ToString()));

        Assert.Same(view, response.Value);
    }

    [Fact]
    public async Task CreateStoresTheLinkForTheUserFromTheHeaderAndReturnsCreated()
    {
        var input = ValidInput();
        var view = ViewOf(input);
        var service = new Mock<IUserGithubsService>();
        service.Setup(item => item.CreateAsync(UserId, input)).ReturnsAsync(Success(view));

        var response = Assert.IsType<CreatedAtActionResult>(
            await CreateController(service.Object).Create(UserId.ToString(), input));

        Assert.Equal(nameof(UserGithubsController.Get), response.ActionName);
        Assert.Same(view, response.Value);
    }

    [Fact]
    public async Task UpdateChangesTheLinkOfTheUserFromTheHeader()
    {
        var input = ValidInput();
        var view = ViewOf(input);
        var service = new Mock<IUserGithubsService>();
        service.Setup(item => item.UpdateAsync(UserId, input)).ReturnsAsync(Success(view));

        var response = Assert.IsType<OkObjectResult>(
            await CreateController(service.Object).Update(UserId.ToString(), input));

        Assert.Same(view, response.Value);
    }

    [Fact]
    public async Task DeleteRemovesTheLinkOfTheUserFromTheHeaderAndReturnsNoContent()
    {
        var service = new Mock<IUserGithubsService>();
        service.Setup(item => item.DeleteAsync(UserId)).ReturnsAsync(UserGithubsServiceStatus.Success);

        var response = await CreateController(service.Object).Delete(UserId.ToString());

        Assert.IsType<NoContentResult>(response);
        service.Verify(item => item.DeleteAsync(UserId), Times.Once);
    }

    [Theory]
    [InlineData(nameof(UserGithubsController.Create))]
    [InlineData(nameof(UserGithubsController.Update))]
    public async Task WriteActionsRejectMissingBodyWithValidationCode(string action)
    {
        var service = new Mock<IUserGithubsService>(MockBehavior.Strict);
        var controller = CreateController(service.Object);

        var response = Assert.IsType<BadRequestObjectResult>(
            await Invoke(controller, action, UserId.ToString(), null));

        Assert.Equal("validation", Property(response.Value, "code"));
        service.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(nameof(UserGithubsController.Create))]
    [InlineData(nameof(UserGithubsController.Update))]
    public async Task WriteActionsReportTheFirstValidationError(string action)
    {
        var validator = new Mock<IObjectModelValidator>();
        validator
            .Setup(item => item.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()))
            .Callback<ActionContext, ValidationStateDictionary, string, object>((context, _, _, _) =>
                context.ModelState.AddModelError("GithubLink", "The GithubLink field is required."));
        var service = new Mock<IUserGithubsService>(MockBehavior.Strict);
        var controller = CreateController(service.Object, validator.Object);

        var response = Assert.IsType<BadRequestObjectResult>(
            await Invoke(controller, action, UserId.ToString(), ValidInput()));

        Assert.Equal("validation", Property(response.Value, "code"));
        Assert.Equal("The GithubLink field is required.", Property(response.Value, "message"));
        service.VerifyNoOtherCalls();
    }

    private static UserGithubsController CreateController(
        IUserGithubsService service,
        IObjectModelValidator validator = null) =>
        new(service)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = validator ?? Mock.Of<IObjectModelValidator>()
        };

    private static Task<IActionResult> Invoke(
        UserGithubsController controller,
        string action,
        string header,
        UserGithubInput input) =>
        action switch
        {
            nameof(UserGithubsController.Get) => controller.Get(header),
            nameof(UserGithubsController.Create) => controller.Create(header, input),
            nameof(UserGithubsController.Update) => controller.Update(header, input),
            nameof(UserGithubsController.Delete) => controller.Delete(header),
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
        };

    private static UserGithubInput ValidInput() =>
        new() { GithubLink = "https://github.com/octocat", Description = "My GitHub profile" };

    private static UserGithubView ViewOf(UserGithubInput input) =>
        new() { UserId = UserId, GithubLink = input.GithubLink, Description = input.Description };

    private static UserGithubsServiceResult<UserGithubView> Success(UserGithubView view) =>
        new(UserGithubsServiceStatus.Success, view);

    private static object Property(object value, string name) =>
        value.GetType().GetProperty(name)!.GetValue(value);
}
