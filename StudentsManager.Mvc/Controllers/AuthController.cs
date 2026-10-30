using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.Auth;
using StudentsManager.Mvc.Domain.Views;
using StudentsManager.Mvc.Services.AppSettings;
using StudentsManager.Mvc.Services.Auth;
using static StudentsManager.Mvc.Services.Auth.AuthConstants;

namespace StudentsManager.Mvc.Controllers
{
    public class AuthController(IAuthService service, IScreenSettingService screenSettingService) : BaseController
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["returnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials model)
        {
            var result = await service.LoginAsync(model);
            if (result == null)
            {
                return RedirectToAction("Problem", new { errorMessage = LoginErrorMessage });
            }

            var returnUrl = TempData["returnUrl"]?.ToString();
            return string.IsNullOrEmpty(returnUrl)
                ? RedirectToHome()
                : RedirectToReturnUrl(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var screenSetting = await screenSettingService.RegistrationScreenSettingAsync;
            if (screenSetting == null)
            {
                return View();
            }
            return screenSetting.Enabled ? View() : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            var result = await service.RegisterAsync(model);
            return result == null
                ? RedirectToAction("Problem", new { errorMessage = RegisterErrorMessage })
                : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await service.LogoutAsync();
            return RedirectToHome();
        }

        [HttpGet]
        public IActionResult Problem(string errorMessage)
        {
            return View(new Error(errorMessage));
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            var screenSetting = await screenSettingService.RegistrationScreenSettingAsync;
            if (screenSetting == null)
            {
                return View();
            }
            return screenSetting.Enabled ? View() : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var result = await service.ResetPasswordAsync(model);
            return result == null
                ? RedirectToAction("Problem", new { errorMessage = UnexpectedErrorMessage })
                : RedirectToHome();
        }
    }
}