using EventManagement.Data;
using EventManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RegistrationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRegistrationForEvent([FromBody] Registration eventRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if an event with this eventid is present
            var ifexistingEvent = await _dbContext.Events.FirstOrDefaultAsync(e =>e.Id == eventRegistration.EventId);

            if (ifexistingEvent == null)
            {
                return BadRequest("Event does not exist,hence cannot be added");
            }

            // Add the Registration to the database
            _dbContext.Registration.Add(eventRegistration);
            await _dbContext.SaveChangesAsync();

            return Ok("Registration added successfully");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var events = await _dbContext.Events.ToListAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
    }
}
