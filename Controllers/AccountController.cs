using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;

namespace GestioneAccounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  

    public class AccountController(ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMediator _mediator = mediator;

        // GET: Account
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAccount = new GetAllAccounts(); 
            var accounts = await mediator.Send(getAccount); 
            return Ok(accounts);
        }

        // POST: Account/Create
        [HttpPost("Create")]
<<<<<<< HEAD
        public async Task<IActionResult> Create([FromBody] CreateAccountDto createAccountDto)
        {
            if (createAccountDto == null)
            {
                return BadRequest("Invalid account data.");
            }

            var createAccount = new CreateAccount { Nome = createAccountDto.Nome };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAccount = await _mediator.Send(createAccount);

            if (createdAccount == null)
            {
                return StatusCode(500, "Account creation failed due to an internal error.");
            }

            return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);    
=======
        public async Task<IActionResult> Create([Bind("Id,Nome")] Account account)
        {
            var createAccount = new CreateAccount { Nome = account.Nome };

            if (ModelState.IsValid)
            {
                var createdAccount = await _mediator.Send(createAccount);

                if (createdAccount != null)
                {
                    return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);
                }
                else
                {
                    return BadRequest("Account creation failed.");
                }
            }

            return BadRequest(ModelState);
>>>>>>> origin/main
        }




<<<<<<< HEAD

=======
>>>>>>> origin/main
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
        [HttpPut]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,Nome")] Account account)
        {
            // Check if the id from the route matches the account id
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

            // Return a BadRequest if the model state is invalid
            return BadRequest(ModelState);
        }

        // DELETE: Account/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var deleteAccountCommand = new DeleteAccount { Id = id };
            var result = await _mediator.Send(deleteAccountCommand);
            return Ok(new { message = "Account eliminato con successo" });
        }

        private bool AccountExists(long? id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
