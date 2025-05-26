using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Configuration.Models.DTOs;
using GestioneAccounts.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using GestioneAccounts.DataAccess;
using Namespace.GestioneAccounts.Configuration;

namespace GestioneAccounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthManagmentController : ControllerBase
    {
        private readonly ILogger<AuthManagmentController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagmentController(
            ILogger<AuthManagmentController> logger,
            ApplicationDbContext context,
            UserManager<Account> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegistrationRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                return BadRequest(new { Message = "Email already registered." });
            }

            var newUser = new Account
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var isCreated = await _userManager.CreateAsync(newUser, model.Password);
            if (isCreated.Succeeded)
            {
                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountRegistrationRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Email = model.Email,
                Token = token
            });
        }

        private string GenerateJwtToken(Account user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
