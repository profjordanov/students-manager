using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.RoboExaminations;

namespace StudentsManager.Mvc.Controllers._Base
{
    internal static class RoboExaminationControllerHelpers
    {
        public static bool IsInvalidInput(ControllerBase controller, object? input)
        {
            return input == null || !controller.TryValidateModel(input);
        }

        public static IActionResult InvalidInput(ControllerBase controller)
        {
            var message = controller.ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error));
            return controller.BadRequest(new
            {
                message = message ?? "Invalid request.",
                code = "validation"
            });
        }

        public static IActionResult Map<T>(
            ControllerBase controller,
            RoboExaminationServiceResult<T> result,
            Func<T, IActionResult> onSuccess,
            int alreadySubmittedStatus = 410)
        {
            if (result is { Status: RoboExaminationServiceStatus.Success, Value: not null })
                return onSuccess(result.Value);

            var (status, code, message) = result.Status switch
            {
                RoboExaminationServiceStatus.InvalidType =>
                    (400, "invalidType", "Type must match a configured examination setting."),
                RoboExaminationServiceStatus.UserNotFound =>
                    (404, "userNotFound", "User not found."),
                RoboExaminationServiceStatus.NoActiveExamination =>
                    (403, "noActiveExamination", "Няма активен изпит."),
                RoboExaminationServiceStatus.RoboQuizRequired =>
                    (403, "roboQuizRequired", "Първо решете RoboQuiz."),
                RoboExaminationServiceStatus.NoVariants =>
                    (404, "noVariants", "Няма налични изпити."),
                RoboExaminationServiceStatus.AlreadySubmitted =>
                    (alreadySubmittedStatus, "alreadySubmitted", "Вече сте предали изпита."),
                RoboExaminationServiceStatus.AttemptNotFound =>
                    (404, "attemptNotFound", "Examination attempt not found."),
                RoboExaminationServiceStatus.Duplicate =>
                    (409, "duplicate", "An examination variant with this type and name already exists."),
                RoboExaminationServiceStatus.NotReevaluable =>
                    (409, "notReevaluable", "This attempt cannot be evaluated again."),
                _ => throw new InvalidOperationException($"Unexpected RoboExamination status: {result.Status}")
            };

            return controller.StatusCode(status, new { message, code });
        }
    }
}
