using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Messaging;
using StudentsManager.Mvc.Services.Messaging;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandboxController(
        IMailService mailService,
        IAzureServiceBusSender serviceBusSender)
        : ControllerBase
    {
        [HttpGet("mail")]
        public async Task<IActionResult> TestMailService()
        {
            var mailRequest = new MailRequest
            {
                ToEmail = "d_yugioh@abv.bg",
                Subject = $"Test #{Guid.NewGuid()}",
                Body = $"Dear, {Guid.NewGuid()}, We are Testing {Guid.NewGuid()}. Bye {Guid.NewGuid()}"
            };
            await mailService.SendEmailAsync(mailRequest);
            return Ok();
        }

        [HttpGet("bus")]
        public async Task<IActionResult> TestAzureServiceBusSender()
        {
            try
            {
                await serviceBusSender.SendAsync(Guid.NewGuid().ToString());
                return Ok("READY!");
            }
            catch (Exception exception)
            {
                return Ok(exception);
            }
        }
    }
}