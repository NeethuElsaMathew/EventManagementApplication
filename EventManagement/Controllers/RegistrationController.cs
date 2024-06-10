using EventManagement.Data;
using EventManagement.Model;
using EventManagement.Model.DTO;
using EventManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Controllers
{
    [Route("api/Registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RegistrationService _registrationService;

        public RegistrationController(ApplicationDbContext dbContext, RegistrationService registrationService)
        {
            _dbContext = dbContext;
            _registrationService = registrationService;
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
    }
}
