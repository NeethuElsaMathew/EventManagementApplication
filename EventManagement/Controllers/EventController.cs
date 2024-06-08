using EventManagement.Data;
using EventManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "EventCreator")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public EventController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddEvent([FromBody] Event eventToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if an event with the same properties already exists
            var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e =>
                e.Name == eventToCreate.Name &&
                e.Description == eventToCreate.Description &&
                e.Location == eventToCreate.Location &&
                e.StartTime == eventToCreate.StartTime &&
                e.EndTime == eventToCreate.EndTime);

            if (existingEvent != null)
            {
                return Conflict("Event already exists,hence cannot be added");
            }

            _dbContext.Events.Add(eventToCreate);
            await _dbContext.SaveChangesAsync();

            return Ok("Event added successfully");
        }

        [HttpGet("{eventId}/registrations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRegistrationsForEvent(int eventId)
        {
            var eventExists = await _dbContext.Events.AnyAsync(e => e.Id == eventId);

            if (!eventExists)
            {
                return NotFound("Event not found");
            }

            var registrations = await _dbContext.Registration
                .Where(r => r.EventId == eventId)
                .ToListAsync();

            return Ok(registrations);
        }
    }
}

