using EventManagement.Model.DTO;
using EventManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;
using EventManagement.Services;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginDTO userLogin)
        {
            var token = await _authService.AuthenticateAsync(userLogin);

            if (token != null)
            {
                return Ok(token);
            }

            return NotFound("User not found or invalid credentials");
        }
    }
}
