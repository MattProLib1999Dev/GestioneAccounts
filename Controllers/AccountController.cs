using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

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
    private readonly IMapper _mapper;

    public AccountController(
        ILogger<AccountController> logger,
        ApplicationDbContext context,
        IMediator mediator,
        IWebHostEnvironment env,
        IMapper mapper)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      accountRepository = new AccountRepository(context);
      _env = env;
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    // POST: api/Account/create
    [HttpPost("create")]
    [Authorize(Roles = "Admin")] // Ensure only Admin or Manager can create accounts
    [AllowAnonymous]
    [ProducesResponseType(typeof(Account), 200)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
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
        OreLavorate = dto.OreLavorate,

          Valori = dto.Valori.Select(v => new Valore
          {
            Nome = v.Nome,
            ValoreStr = v.valoreString,
            Voce = v.voce,
            DataCreazione = v.DataCreazione,
            Descrizione = v.Descrizione,
            ValoreNumerico = v.ValoreNumerico,
            AccountId = v.AccountId
          }).ToList()
      };
      var accountDto = _mapper.Map<CreateAccountDto>(account);


      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();

      return Ok(accountDto);
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
public async Task<IActionResult> Search([FromQuery] string nome)
{
    if (string.IsNullOrWhiteSpace(nome))
    {
        return BadRequest(new { message = "Il nome è obbligatorio." });
    }

    // 🔍 Controllo diretto se esiste almeno un account con quel nome
    var exists = await _context.Accounts.AnyAsync(a => a.Nome == nome);

    if (!exists)
    {
        return NotFound(new { message = "Nessun account trovato con questo nome." });
    }

    // ✅ Se esiste, prosegui con MediatR
    var query = new SearchAccount { Nome = nome };
    var result = await _mediator.Send(query);

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
        if (accounts == null || accounts.Count != 0)
        {
          return NotFound(new { message = "No accounts found." });
        }
        // Map accounts to DTOs if necessary
        var accountDto = accounts.Select(a => _mapper.Map<CreateAccountDto>(a)).ToList();
        return Ok(accountDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error retrieving accounts");
        return StatusCode(500, new { message = "Internal server error" });
      }
    }

    // POST: account/approvaOreLavorate
    [HttpPost("approvaOreLavorate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApprovaOreLavorate([FromBody] CreateAccountDto CreateAccountDto)
    {
      if (CreateAccountDto == null)
      {
        return BadRequest("Account data is required.");
      }

      // Trova l'account esistente
      var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == CreateAccountDto.Id.ToString());
      if (account == null)
      {
        return NotFound("Account not found.");
      }

      // Aggiorna le ore lavorate
      var accountDto = _mapper.Map<CreateAccountDto>(account);

      // Salva le modifiche nel database
      _context.Accounts.Update(account);
      await _context.SaveChangesAsync();

      return Ok(accountDto);
    }

  }
}
