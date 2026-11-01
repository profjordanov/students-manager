using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(ManagerDbContext dbContext) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var result = await dbContext
            .Events
            .Where(@event => @event.UserId == userId)
            .ToListAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventInput input)
    {
        var entity = new Event
        {
            Id = Guid.NewGuid(),
            UserId = input.UserId,
            DatetimeUtc = DateTime.UtcNow,
            Type = input.Type,
            Data = input.Data
        };
        dbContext.Events.Add(entity);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByUser), new { userId = entity.UserId }, entity);
    }
}
