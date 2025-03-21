using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Posts.CommandHandlers;
using Amazon.Common;
using Microsoft.EntityFrameworkCore;

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
        public OperationObjectResultStatus Status { get; set; }

    // POST: api/Account/create
    [HttpPost("create")]
    public IActionResult Login([FromBody] Account request)
    {
      if (request == null || string.IsNullOrWhiteSpace(request.Nome) ||
          string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.voce))
      {
        _logger.LogWarning("Invalid login request: missing credentials.");
        return BadRequest("Invalid request. Username, voice, and value are required.");
      }

      try
      {
        _logger.LogInformation("User logged in successfully.");
        return Ok(request);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred during login.");
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login. Please try again later.");
      }
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            // Recupera tutti gli account dal database usando Entity Framework.
            var accounts = await _context.Accounts.ToListAsync();

            if (accounts == null || accounts.Count == 0)
            {
                // Se non ci sono account, ritorna un messaggio 404 Not Found.
                return NotFound(new { message = "No accounts found." });
            }

            // Ritorna la lista degli account con status 200 OK.
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            // Gestisce gli errori e ritorna un errore 500 se c'è un problema.
            _logger.LogError(ex, "An error occurred while retrieving all accounts.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the accounts.");
        }
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
        public async Task<IActionResult> Search([FromQuery] string nome, [FromQuery] DateTime? dataCreazione, [FromQuery] string? valoreString, [FromQuery] string? voce)
        {
            var query = new SearchItemsQuery
            {
                Nome = nome,
                DataCreazione = dataCreazione,
                ValoreString = valoreString,
                Voce = voce
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // 🔢 2. API per la paginazione
        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var query = new PaginateItemsQuery { Page = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // 🔀 3. API per l'ordinamento
        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string? nome, [FromQuery] DateTime? dataCreazione, [FromQuery] string? valoreString, [FromQuery] string? voce, [FromQuery] string? orderBy, [FromQuery] bool descending = false)
        {
            var query = new SortAccountsQuery
            {
                Nome = nome,
                DataCreazione = dataCreazione,
                ValoreString = valoreString,
                Voce = voce,
                OrderBy = orderBy ?? "Nome",
                Descending = descending
            };

            var result = await _mediator.Send(query); // ✅ Ora stai inviando un oggetto che implementa IRequest<T>
            return Ok(result);
        }



    }
}
