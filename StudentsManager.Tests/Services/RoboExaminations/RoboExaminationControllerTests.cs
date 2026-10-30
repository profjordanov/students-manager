using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Options;
using Moq;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.RoboExaminations;
using StudentsManager.Mvc.Domain.Views.RoboExaminations;
using StudentsManager.Mvc.Services.RoboExaminations;
using StudentsManager.Mvc.Settings;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

public class RoboExaminationControllerTests
{
    [Theory]
    [InlineData(RoboExaminationServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(RoboExaminationServiceStatus.NoActiveExamination, 403, "noActiveExamination")]
    [InlineData(RoboExaminationServiceStatus.RoboQuizRequired, 403, "roboQuizRequired")]
    [InlineData(RoboExaminationServiceStatus.NoVariants, 404, "noVariants")]
    [InlineData(RoboExaminationServiceStatus.AlreadySubmitted, 410, "alreadySubmitted")]
    public async Task GetMapsServiceStatuses(
        RoboExaminationServiceStatus status,
        int expectedStatus,
        string expectedCode)
    {
        var service = new Mock<IRoboExaminationService>();
        service.Setup(item => item.GetOrAssignAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new RoboExaminationServiceResult<RoboExaminationView>(status, null));
        var controller = new RoboExaminationController(service.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };

        var response = Assert.IsType<ObjectResult>(await controller.Get(Guid.NewGuid().ToString()));

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedCode, response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Fact]
    public async Task GetRejectsInvalidUserIdWithStableCode()
    {
        var controller = new RoboExaminationController(Mock.Of<IRoboExaminationService>())
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };

        var response = Assert.IsType<BadRequestObjectResult>(await controller.Get("invalid"));

        Assert.Equal("invalidUserId", response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Theory]
    [InlineData(RoboExaminationServiceStatus.InvalidType, 400, "invalidType")]
    [InlineData(RoboExaminationServiceStatus.Duplicate, 409, "duplicate")]
    public async Task CreateMapsServiceStatuses(
        RoboExaminationServiceStatus status,
        int expectedStatus,
        string expectedCode)
    {
        var service = new Mock<IRoboExaminationService>();
        service.Setup(item => item.CreateAsync(It.IsAny<RoboExaminationInput>()))
            .ReturnsAsync(new RoboExaminationServiceResult<RoboExaminationSummaryView>(status, null));
        var controller = new RoboExaminationController(service.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };
        var input = new RoboExaminationInput
        {
            Type = "First",
            Variant = "A",
            Questions = [new() { Id = "q1", Text = "Question", Type = "text" }]
        };

        var response = Assert.IsType<ObjectResult>(await controller.Create(input));

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedCode, response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Fact]
    public async Task ResultsPostMapsAlreadySubmittedToConflict()
    {
        var service = new Mock<IRoboExaminationService>();
        service.Setup(item => item.SubmitAsync(
                It.IsAny<RoboExaminationSubmissionInput>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RoboExaminationServiceResult<RoboExaminationSubmissionView>(
                RoboExaminationServiceStatus.AlreadySubmitted,
                null));
        var controller = new RoboExaminationResultsController(service.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };

        var response = Assert.IsType<ObjectResult>(await controller.Create(
            new RoboExaminationSubmissionInput
            {
                UserId = Guid.NewGuid(),
                AttemptId = 1,
                Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "answer" }]
            }));

        Assert.Equal(409, response.StatusCode);
        Assert.Equal("alreadySubmitted", response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Theory]
    [InlineData(RoboExaminationServiceStatus.UserNotFound, 404, "userNotFound")]
    [InlineData(RoboExaminationServiceStatus.AttemptNotFound, 404, "attemptNotFound")]
    [InlineData(RoboExaminationServiceStatus.NoActiveExamination, 403, "noActiveExamination")]
    [InlineData(RoboExaminationServiceStatus.RoboQuizRequired, 403, "roboQuizRequired")]
    [InlineData(RoboExaminationServiceStatus.InvalidAnswers, 400, "invalidAnswers")]
    [InlineData(RoboExaminationServiceStatus.EvaluationSuperseded, 409, "evaluationSuperseded")]
    public async Task ResultsPostMapsSubmissionErrors(
        RoboExaminationServiceStatus status,
        int expectedStatus,
        string expectedCode)
    {
        var service = new Mock<IRoboExaminationService>();
        service.Setup(item => item.SubmitAsync(
                It.IsAny<RoboExaminationSubmissionInput>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RoboExaminationServiceResult<RoboExaminationSubmissionView>(status, null));
        var controller = new RoboExaminationResultsController(service.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };
        var input = new RoboExaminationSubmissionInput
        {
            UserId = Guid.NewGuid(),
            AttemptId = 1,
            Answers = [new RoboExaminationAnswerInput { QuestionId = "q1", Answer = "answer" }]
        };

        var response = Assert.IsType<ObjectResult>(await controller.Create(input));

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedCode, response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Theory]
    [InlineData(RoboExaminationServiceStatus.AttemptNotFound, 404, "attemptNotFound")]
    [InlineData(RoboExaminationServiceStatus.NotReevaluable, 409, "notReevaluable")]
    [InlineData(RoboExaminationServiceStatus.EvaluationSuperseded, 409, "evaluationSuperseded")]
    public async Task ReevaluateMapsServiceStatuses(
        RoboExaminationServiceStatus status,
        int expectedStatus,
        string expectedCode)
    {
        var service = new Mock<IRoboExaminationService>();
        service.Setup(item => item.ReevaluateAsync(10))
            .ReturnsAsync(new RoboExaminationServiceResult<RoboExaminationSubmissionView>(status, null));
        var controller = new RoboExaminationResultsController(service.Object)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
        };

        var response = Assert.IsType<ObjectResult>(await controller.Reevaluate(10));

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedCode, response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }

    [Fact]
    public async Task ResultsPostRejectsNullBodyWithValidationCode()
    {
        var controller = new RoboExaminationResultsController(Mock.Of<IRoboExaminationService>())
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() },
            ObjectValidator = Mock.Of<IObjectModelValidator>()
        };

        var response = Assert.IsType<BadRequestObjectResult>(await controller.Create(null));

        Assert.Equal("validation", response.Value!.GetType().GetProperty("code")!.GetValue(response.Value));
    }
}