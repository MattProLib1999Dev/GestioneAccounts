using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using GestioneAccounts.BE.Domain; // Ensure this is the correct namespace for ApplicationDbContext
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GestioneAccounts.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;
    public readonly AccountRepository accountRepository;
    private readonly IWebHostEnvironment _env;

    public AccountController(
        ILogger<AccountController> logger,
        ApplicationDbContext context,
        IMediator mediator,
        IWebHostEnvironment env)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      accountRepository = new AccountRepository(context);
      _env = env;
    }

    // POST: api/Account/create
    [HttpPost("create")]
    [Authorize(Roles = "Admin,Manager")] // Ensure only Admin or Manager can create accounts
    [AllowAnonymous]
    [ProducesResponseType(typeof(Account), 200)]
    public async Task<IActionResult> CreateAccount([FromBody] PostAccountDto dto)
    {
      var account = new Account
      {
        UserName = dto.UserName,
        NormalizedUserName = dto.NormalizedUserName,
        Email = dto.Email,
        NormalizedEmail = dto.NormalizedEmail,
        EmailConfirmed = dto.EmailConfirmed,
        PasswordHash = dto.PasswordHash,
        SecurityStamp = dto.SecurityStamp,
        ConcurrencyStamp = dto.ConcurrencyStamp,
        PhoneNumber = dto.PhoneNumber,
        PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
        TwoFactorEnabled = dto.TwoFactorEnabled,
        LockoutEnd = dto.LockoutEnd,
        LockoutEnabled = dto.LockoutEnabled,
        AccessFailedCount = dto.AccessFailedCount,
        Nome = dto.Nome,
        Voce = dto.Voce,
        ValoreString = dto.ValoreString,
        DataCreazione = dto.DataCreazione,

        Valori = dto.Valori.Select(v => new Valore
        {
          Nome = v.Nome,
          ValoreStr = v.valoreString,
          Voce = v.voce,
          DataCreazione = v.DataCreazione,
          Descrizione = v.Descrizione,
          ValoreNumerico = v.ValoreNumerico,
          AccountId = (string)v.AccountId // Set to null, will be set by EF when saving
        }).ToList()
      };

      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();

      return Ok(account);
    }


    // GET: api/Account/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
      var getAccount = new GetAccountById { Id = id };
      var account = await _mediator.Send(getAccount);

      if (account == null)
      {
        return NotFound();
      }

      return Ok(account);
    }

    // PUT: api/Account/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(int id, [FromBody] UpdateAccountCommand command)
    {
      if (command == null)
      {
        return BadRequest("Account data is required.");
      }

      command.Id = id;
      var updatedAccount = await _mediator.Send(command);

      if (updatedAccount == null)
      {
        return NotFound("Account not found.");
      }

      return Ok(updatedAccount);
    }

    // DELETE: api/Account/Delete/{id}
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
      var deleteAccountCommand = new DeleteAccount { Id = id };
      var result = await _mediator.Send(deleteAccountCommand);

      if (result != null)
      {
        return Ok(new { message = "Account eliminato con successo" });
      }

      return BadRequest("Account deletion failed.");
    }

    private bool AccountExists(int id)
    {
      return _context.Accounts.Any(e => e.Id == id.ToString());
    }

    // GET: api/Account/search
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string nome, [FromQuery] DateTime dataCreazione, [FromQuery] string valoreString)
    {
      var query = new SearchAccount
      {
        Nome = nome,
        DataCreazione = dataCreazione,
        ValoreString = valoreString,
      };

      var result = await _mediator.Send(query);
      if (result == null)
      {
        return NotFound(new { message = "No accounts found." });
      }
      return Ok(result);
    }

    // GET: api/Account/orderByName
    [HttpGet("orderByName")]
    public async Task<IActionResult> OrderByName()
    {
      var accounts = await _context.Accounts
          .OrderBy(a => a.Nome)
          .ToListAsync();
      return Ok(accounts);
    }

    [HttpPost("upload")]
    public IActionResult UploadBase64Image([FromBody] ImageUploadRequest request)
    {
      try
      {
        if (string.IsNullOrEmpty(_env.WebRootPath))
        {
          return StatusCode(500, new { error = "WebRootPath is null. Verifica che wwwroot sia configurata correttamente." });
        }
        Console.WriteLine("WebRootPath = " + _env.WebRootPath);


        var image = Base64Image.Parse(request.Base64Image);

        string extension = image.ContentType switch
        {
          "image/png" => ".png",
          "image/jpeg" => ".jpg",
          "image/gif" => ".gif",
          _ => ".bin"
        };

        string fileName = $"matt{Guid.NewGuid()}{extension}";
        string folderPath = Path.Combine(_env.WebRootPath, "assets", "img");
        Directory.CreateDirectory(folderPath);

        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, fileName);

        System.IO.File.WriteAllBytes(filePath, image.FileContents);

        return Ok(new { fileName });
      }
      catch (Exception ex)
      {
        return BadRequest(new { error = ex.Message });
      }
    }

    //getall
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAccounts()
    {
      try
      {
        var accounts = await _context.Accounts.ToListAsync();
        if (accounts == null || !accounts.Any())
        {
          return NotFound(new { message = "No accounts found." });
        }
        return Ok(accounts);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error retrieving accounts");
        return StatusCode(500, new { message = "Internal server error" });
      }
    }
  }
}
