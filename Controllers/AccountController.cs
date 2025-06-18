using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
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

        public AccountController(
            ILogger<AccountController> logger,
            ApplicationDbContext context,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            accountRepository = new AccountRepository(context);
        }

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

                request.Id = 0;
                request.voce = string.Empty;
                request.dataCreazione = DateTime.Now;

                var existingAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Nome == request.Nome && a.voce == request.voce && a.valoreString == request.valoreString);
                if (existingAccount != null)
                {
                    return Conflict(new { message = "Account already exists." });
                }

                _context.Entry(request).State = EntityState.Added;
                _context.Accounts.Add(request);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the account.");
                return StatusCode(500, new { message = "An error occurred." });
            }
        }

        // GET: api/Account/all
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

        private bool AccountExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
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
    }
}
