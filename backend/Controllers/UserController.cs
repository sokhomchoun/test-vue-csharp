using backend.Data;
using backend.Models;
using backend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public UserController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult AddUsers(AddUser addUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(addUser.Email) || string.IsNullOrWhiteSpace(addUser.Password))
                {
                    return BadRequest("Email and Password are required.");
                }
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = addUser.Email,
                    Password = addUser.Password
                };
                dbContext.users.Add(user);
                dbContext.SaveChanges();
                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }   
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser loginUser)
        {
            try
            {
                var user = dbContext.users.FirstOrDefault(u => u.Email == loginUser.Email);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                if (user.Password != loginUser.Password)
                {
                    return Unauthorized("Incorrect password");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("THIS_IS_A_VERY_LONG_SECURE_KEY_123456");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "token",
                    Audience = "token",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    status = true,
                    message = "Login successful",
                    token = jwtToken,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
    }
}
