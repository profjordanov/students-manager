using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Views.Home;
using StudentsManager.Mvc.Domain.Views.Surveys;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Surveys;
using Xunit;

namespace StudentsManager.Tests.Services;

public class HomeControllerTests
{
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Mock<IExaminationSettingsService> _settingsService = new();
    private readonly Mock<IPrincipalService> _principalService = new();
    private readonly Mock<ISurveysService> _surveysService = new();

    public HomeControllerTests()
    {
        _settingsService.Setup(service => service.GetViewAsync())
            .ReturnsAsync(new CourseExaminationSettingsView(false, false));
        _principalService.Setup(service => service.IsSignedInByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()))
            .Returns(false);
    }

    [Fact]
    public async Task IndexHidesSurveyForSignedOutUsers()
    {
        var result = Assert.IsType<ViewResult>(await CreateController().Index());

        Assert.Equal(false, result.ViewData["ShowSurvey"]);
        _principalService.Verify(service => service.GetUserIdByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()), Times.Never);
        _surveysService.Verify(service => service.GetByUserIdAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task IndexShowsSurveyForSignedInUsersWithoutSubmission()
    {
        SetupSignedInUser();
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync((SurveyResultView)null);

        var result = Assert.IsType<ViewResult>(await CreateController().Index());

        Assert.Equal(true, result.ViewData["ShowSurvey"]);
    }

    [Fact]
    public async Task IndexHidesSurveyForSignedInUsersWithSubmission()
    {
        SetupSignedInUser();
        _surveysService.Setup(service => service.GetByUserIdAsync(_userId))
            .ReturnsAsync(new SurveyResultView());

        var result = Assert.IsType<ViewResult>(await CreateController().Index());

        Assert.Equal(false, result.ViewData["ShowSurvey"]);
    }

    private void SetupSignedInUser()
    {
        _principalService.Setup(service => service.IsSignedInByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()))
            .Returns(true);
        _principalService.Setup(service => service.GetUserIdByClaimsPrincipal(It.IsAny<ClaimsPrincipal>()))
            .Returns(_userId);
    }

    private HomeController CreateController()
    {
        return new HomeController(_settingsService.Object, _principalService.Object, _surveysService.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };
    }
}