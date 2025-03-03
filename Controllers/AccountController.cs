using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using GestioneAccounts.BE.Domain.Models;

namespace GestioneAccounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    // GET: Account
    [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAccount = new GetAllAccounts();
            var accounts = await _mediator.Send(getAccount);
            return Ok(accounts);
        }

        // POST: Account/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ValoriCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var account = createDto.Account;
            var valori = createDto.Valori;

            // Validate account and valori, create the records, etc.

            return Ok();
        }


        // GET: Account/Edit/5
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

        // PUT: Account/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome")] Account account)
        {
            // Ensure the id from route matches the account id
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateAccount = new UpdateAccount
                {
                    AccountId = account.Id,
                    Nome = account.Nome
                };

                var updatedAccount = await _mediator.Send(updateAccount);

                if (updatedAccount != null)
                {
                    return Ok(updatedAccount);
                }

                return BadRequest("Account update failed.");
            }

            return BadRequest(ModelState);
        }

        // DELETE: Account/Delete/5
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
    }
}
