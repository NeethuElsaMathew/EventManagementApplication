using EventManagement.Data;
using EventManagement.Model;
using EventManagement.Model.DTO;
using EventManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RegistrationService _registrationService;
        private ResponseDTO _response;

        public RegistrationController(ApplicationDbContext dbContext, RegistrationService registrationService)
        {
            _dbContext = dbContext;
            _registrationService = registrationService;
            _response = new ResponseDTO();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRegistrationForEvent([FromBody] Registration eventRegistration)
        {
            var response = await _registrationService.AddRegistrationAsync(eventRegistration);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);


        }

        [HttpGet("events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTO> GetAllEvents()
        {
            try
            {
                var events = await _dbContext.Events.ToListAsync();
                _response.Result = events;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Error retrieving data: {ex.Message}";
            }
            return _response;
        }
    }
}
