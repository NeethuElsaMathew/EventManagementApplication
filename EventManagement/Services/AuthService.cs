using EventManagement.Data;
using EventManagement.Model.DTO;
using EventManagement.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<string> AuthenticateAsync(LoginDTO userLogin)
        {
            var user = await AuthenticateUserAsync(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }

            return null;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> AuthenticateUserAsync(LoginDTO userLogin)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userLogin.Username.ToLower() && u.Password == userLogin.Password);

            return currentUser;
        }
    }

}
