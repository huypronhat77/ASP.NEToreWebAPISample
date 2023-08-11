using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using WebAPISampleApp.Data;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private MyDbContext _context;
        private IConfiguration _configuration;

        public UsersController(IConfiguration configuration, MyDbContext context)
        {
            _context = context;  
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Validat(LoginModel loginModel)
        {
            var user = _context.users.SingleOrDefault(u => u.UserName.Equals(loginModel.UserName)
            && u.Password.Equals(loginModel.Password));

            if (user == null) { 
                return NotFound("Invalid Username or Password");
            }

            return Ok(user);
        }

        private string GenerateToken(User user)
        {
            if (user == null)
            {
                return null;
            }
            var secretKey = _configuration["AppSetting:SecretKey"];
            var jwtSecurityHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserName", user.UserName),
                    new Claim("Id", user.Id.ToString()),

                    //role
                    new Claim("RoleId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.EcdsaSha512Signature)
            };

            var token = jwtSecurityHandler.CreateToken(tokenDescription);
            return jwtSecurityHandler.WriteToken(token);
          }
        }
    }

