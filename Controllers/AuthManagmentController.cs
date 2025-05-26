using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Configuration.Models.DTOs;
using GestioneAccounts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Namespace.GestioneAccounts.Configuration;
using Namespace.GestioneAccounts.Configuration.Models.DTOs;

namespace GestioneAccounts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthManagmentController(
    ILogger<AuthManagmentController> logger,
    ApplicationDbContext context,
    UserManager<AccountRegistrationRequestDto> userManager,
    IOptionsMonitor<JwtConfig> optionsMonitor) : ControllerBase
{
    private readonly ILogger<AuthManagmentController> _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<AccountRegistrationRequestDto> _userManager = userManager;
    private readonly JwtConfig _jwtConfig = optionsMonitor.CurrentValue;


    // Implementazione dei metodi per la gestione dell'autenticazione e autorizzazione

    // Esempio di metodo per la registrazione di un nuovo utente

    [HttpPost("register")]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] AccountRegistrationRequestDto model)
    {
        if (!ModelState.IsValid)
        {
          // check if email already exists
          var emailExist = await _userManager.FindByEmailAsync(model.Email);
          if(emailExist != null)
            return BadRequest (new { Message = "User registered successfully.", ModelState });

            var newUser = new AccountRegistrationRequestDto
            {
                Email = model.Email,
                Name = model.Email,
            };

            var isCreated = await _userManager.CreateAsync(newUser, model.Password);
            if (isCreated.Succeeded)
            {
                return Ok(new AccountRegistrationRequestDto
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                });
            }

            return BadRequest (isCreated.Errors.Select(x => x.Description).ToList());
        }

        return BadRequest("Invalid request data.");

    }

    // Esempio di metodo per il login di un utente
    [HttpPost("login")]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] AccountLoginRequestDto model)
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
        return Ok(new AccountLoginRequestDto
        {
            Email = model.Email,
            Password = model.Password,

        });
    }

  private string GenerateJwtToken(AccountRegistrationRequestDto user)
  {
    var jwtTokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(
        [
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ]),
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
