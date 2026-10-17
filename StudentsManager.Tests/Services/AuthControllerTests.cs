using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Auth;
using StudentsManager.Mvc.Services.AppSettings;
using StudentsManager.Mvc.Services.Auth;
using Xunit;

namespace StudentsManager.Tests.Services;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authService = new();
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _authService.Setup(service => service.LoginAsync(It.IsAny<Credentials>()))
            .ReturnsAsync(new User());

        var httpContext = new DefaultHttpContext();
        _controller = new AuthController(_authService.Object, Mock.Of<IScreenSettingService>())
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
            TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
        };
    }

    [Theory]
    [InlineData("/Survey")]
    [InlineData("/Survey/Results?sort=recent")]
    public async Task LoginRedirectsHomeWhenSurveyWasRequested(string returnUrl)
    {
        _controller.TempData["returnUrl"] = returnUrl;

        var result = Assert.IsType<RedirectToActionResult>(await _controller.Login(new Credentials()));

        Assert.Equal("Home", result.ControllerName);
        Assert.Equal("Index", result.ActionName);
    }
}