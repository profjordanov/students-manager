using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Inputs.Forum;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Forum;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidoController(IForumService service, ManagerDbContext dbContext) : ControllerBase
    {
        // GET: api/slido?limit=20&skip=0
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int limit, [FromQuery] int skip)
        {
            var forumQuestions = await service.GetAsync(limit, skip);
            var result = forumQuestions.Select(question => new QuestionCommentsView(
                question.Id,
                question.Description,
                question.Comments?.Select(comment => comment.Description).ToArray()
            )).ToList();
            return Ok(result);
        }

        // POST: api/slido/comment
        [HttpPost("comment")]
        public async Task<IActionResult> PostComment([FromBody] CommentInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Description) || input.ForumQuestionId == 0)
                return BadRequest("Comment cannot be empty");

            if (!await UserExistsAsync(input.UserId))
                return Unauthorized("A valid signed-in user is required");

            await service.SaveCommentAsync(
                new Comment { Description = input.Description, ForumQuestionId = input.ForumQuestionId },
                input.UserId);

            return Ok("Comment posted successfully");
        }
        
        // GET: api/slido/questions?limit=20&skip=0
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions([FromQuery] int limit, [FromQuery] int skip)
        {
            var result = await service.GetSlidoQuestionsAsync(limit, skip);
            return Ok(result);
        }

        // POST: api/slido/question
        [HttpPost("question")]
        public async Task<IActionResult> PostQuestion([FromBody] QuestionInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Question))
                return BadRequest("Question cannot be empty");

            if (!await UserExistsAsync(input.UserId))
                return Unauthorized("A valid signed-in user is required");

            await service.SaveQuestionAsync(input.Question, input.UserId);

            return Ok("Question posted successfully");
        }

        private Task<bool> UserExistsAsync(Guid userId) =>
            userId == Guid.Empty
                ? Task.FromResult(false)
                : dbContext.Users.AnyAsync(user => user.Id == userId);
    }

    public record struct QuestionInput(string Question, Guid UserId);
    public record struct CommentInput(string Description, int ForumQuestionId, Guid UserId);
    public record struct QuestionCommentsView(int ForumQuestionId, string ForumQuestionDescription, string[]? Comments);
}
