using Azure;
using EventManagement.Data;
using EventManagement.Model;
using EventManagement.Model.DTO;
using EventManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    [Route("api/Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly EventService _eventService;
        private ResponseDTO _response;

        public EventController(ApplicationDbContext dbContext, EventService eventService)
        {
            _dbContext = dbContext;
            _eventService = eventService;
            _response = new ResponseDTO();

        }

        [HttpPost]
        [Authorize(Roles = "EVENTCREATOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddEvent([FromBody] Event eventToCreate)
        {
            var result = await _eventService.AddEventAsync(eventToCreate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("registrations/{eventId}")]
        [Authorize(Roles = "EVENTCREATOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRegistrationsForEvent(int eventId)
        {
            if (eventId.GetType() != typeof(int))
            {
                return BadRequest("EventId must be an integer.");
            }

            var response = await _eventService.GetRegistrationsForEventResponseAsync(eventId);

            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("all/events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTO> GetAllEvents()
        {
            try
            {
                var events = await _dbContext.Events.ToListAsync();
                _response.Result = events;
            }
            catch 
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data";
            }
            return _response;
        }
    }
}

