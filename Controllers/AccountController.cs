using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Posts.CommandHandlers;
using Amazon.Common;
using Microsoft.EntityFrameworkCore;
using GestioneAccounts.DataAccess.Repositories;

namespace GestioneAccounts.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController(ILogger<AccountController> logger, ApplicationDbContext context, IMediator mediator) : Controller
  {
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context), "Context is not being injected!");
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Mediator is not being injected!");
    private readonly ILogger<AccountController> _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger is not being injected!");
    public string Message { get; set; } = string.Empty;
    public readonly AccountRepository accountRepository = new(context);
    public OperationObjectResultStatus Status { get; set; }

    // POST: api/Account/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] Account request)
    {
      if (request == null || string.IsNullOrWhiteSpace(request.Nome))
      {
        return BadRequest(new { message = "Invalid request. Account data is required." });
      }

      try
      {
        // Assicura che gli ID siano nulli per evitare problemi con il tracking di Entity Framework
        request.Id = 0;

        request.voce = string.Empty; // Assicurati che Voce sia inizializzato
        request.dataCreazione = DateTime.Now; // Imposta la data di creazione a ora corrente

        // Controlla se l'account esiste già
        var existingAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Nome == request.Nome && a.voce == request.voce && a.valoreString == request.valoreString);
        if (existingAccount != null)
        {
          return Conflict(new { message = "Account already exists." });
        }

        // Aggiungi l'account al contesto
        _context.Entry(request).State = EntityState.Added; // Imposta lo stato dell'entità a "Added"

        _context.Accounts.Add(request); // Aggiunge l'account al DB
        await _context.SaveChangesAsync(); // Salva nel DB

        return CreatedAtAction(nameof(GetAll), new { id = request.Id }, request);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error occurred while creating the account.");
        return StatusCode(500, new { message = "An error occurred." });
      }
    }


[HttpGet("all")]
public async Task<IActionResult> GetAll()
{
    var account = await _context.Accounts.ToListAsync();

    if (account == null || !account.Any())
        return NotFound(new { message = "Nessun valore trovato." });

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
    public async Task<IActionResult> UpdateAccount(long id, [FromBody] UpdateAccountCommand command)
    {
      if (command == null)
      {
        return BadRequest("Account data is required.");
      }

      // Imposta l'ID nel comando
      command.Id = id;

      // Invia il comando al MediatR
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

    // Utility method to check if account exists
    private bool AccountExists(long id)
    {
      return _context.Accounts.Any(e => e.Id == id);
    }

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



    // 🔀 3. API per l'ordinamento
    [HttpGet("orderByName")]
    public async Task<IActionResult> OrderByName()
    {
      var accounts = await _context.Accounts
          .OrderBy(a => a.Nome)
          .ToListAsync();
      return Ok(accounts);
    }



  }
}

