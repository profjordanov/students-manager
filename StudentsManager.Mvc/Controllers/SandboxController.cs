using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Messaging;
using StudentsManager.Mvc.Services.AI;
using StudentsManager.Mvc.Services.Messaging;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandboxController(
        IMailService mailService,
        IAzureServiceBusSender serviceBusSender,
        IFoundryAgent foundryAgent)
        : ControllerBase
    {
        private readonly IMailService _mailService = mailService;
        private readonly IAzureServiceBusSender _serviceBusSender = serviceBusSender;
        private readonly IFoundryAgent _foundryAgent = foundryAgent;

        [HttpGet("mail")]
        public async Task<IActionResult> TestMailService()
        {
            var mailRequest = new MailRequest
            {
                ToEmail = "d_yugioh@abv.bg",
                Subject = $"Test #{Guid.NewGuid()}",
                Body = $"Dear, {Guid.NewGuid()}, We are Testing {Guid.NewGuid()}. Bye {Guid.NewGuid()}"
            };
            await _mailService.SendEmailAsync(mailRequest);
            return Ok();
        }

        [HttpGet("bus")]
        public async Task<IActionResult> TestAzureServiceBusSender()
        {
            try
            {
                await _serviceBusSender.SendAsync(Guid.NewGuid().ToString());
                return Ok("READY!");
            }
            catch (Exception exception)
            {
                return Ok(exception);
            }
        }

        [HttpGet("foundry")]
        public async Task<IActionResult> TestFoundryAgent([FromQuery] string message = "Hello, what can you do?")
        {
            try
            {
                var result = await _foundryAgent.Test();
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/chat")]
        public async Task<IActionResult> TestChatWithSystemPrompt(
            [FromQuery] string systemPrompt = "You are a helpful assistant.",
            [FromQuery] string userMessage = "Hello, what can you do?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithSystemPrompt(systemPrompt, userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/chat-options")]
        public async Task<IActionResult> TestChatWithOptions(
            [FromQuery] string userMessage = "Hello, what can you do?",
            [FromQuery] float temperature = 0.7f,
            [FromQuery] int maxTokens = 500)
        {
            try
            {
                var result = await _foundryAgent.ChatWithOptions(userMessage, temperature, maxTokens);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/stream")]
        public async Task TestStreamChat([FromQuery] string userMessage = "Hello, what can you do?")
        {
            Response.ContentType = "text/plain";
            try
            {
                await foreach (var chunk in _foundryAgent.StreamChat(userMessage))
                {
                    await Response.WriteAsync(chunk);
                    await Response.Body.FlushAsync();
                }
            }
            catch (Exception exception)
            {
                await Response.WriteAsync($"Error: {exception.Message}");
            }
        }

        [HttpGet("foundry/multi-turn")]
        public async Task<IActionResult> TestMultiTurnConversation(
            [FromQuery] string userMessage = "What are best practices for student management?")
        {
            try
            {
                var messages = new List<(string Role, string Content)>
                {
                    ("system", "You are a helpful assistant for a student management system."),
                    ("user", userMessage)
                };
                var result = await _foundryAgent.MultiTurnConversation(messages);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/json-output")]
        public async Task<IActionResult> TestChatWithJsonOutput(
            [FromQuery] string userMessage = "Analyze this student feedback: The course was excellent but the schedule was too tight.")
        {
            try
            {
                var result = await _foundryAgent.ChatWithJsonOutput(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/tool-calling")]
        public async Task<IActionResult> TestChatWithToolCalling(
            [FromQuery] string userMessage = "What is the grade for student S001 in course CS101?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithToolCalling(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/summarize")]
        public async Task<IActionResult> TestSummarizeWithStopSequences(
            [FromQuery] string text = "The student management system provides tools for tracking grades, attendance, and course enrollment.")
        {
            try
            {
                var result = await _foundryAgent.SummarizeWithStopSequences(text);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/analyze-image")]
        public async Task<IActionResult> TestAnalyzeImage(
            [FromQuery] string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/300px-PNG_transparency_demonstration_1.png",
            [FromQuery] string question = "What do you see in this image?")
        {
            try
            {
                var result = await _foundryAgent.AnalyzeImage(new Uri(imageUrl), question);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/reproducible")]
        public async Task<IActionResult> TestReproducibleChat(
            [FromQuery] string userMessage = "List 3 benefits of online learning.",
            [FromQuery] long seed = 42)
        {
            try
            {
                var result = await _foundryAgent.ReproducibleChat(userMessage, seed);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/logprobs")]
        public async Task<IActionResult> TestChatWithLogprobs(
            [FromQuery] string userMessage = "What is the capital of France?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithLogprobs(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/nucleus-sampling")]
        public async Task<IActionResult> TestGenerateWithNucleusSampling(
            [FromQuery] string userMessage = "Write a short creative paragraph about a student's first day.",
            [FromQuery] float topP = 0.1f)
        {
            try
            {
                var result = await _foundryAgent.GenerateWithNucleusSampling(userMessage, topP);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }
    }
}