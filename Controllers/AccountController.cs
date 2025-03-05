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
      var getAccount = new GetAllAccountsQuery();
      var accounts = await _mediator.Send(getAccount);
      return Ok(accounts);
    }



    // POST: Account/Create
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] string nome, [FromForm] string valoreString, [FromForm] string voce, [FromForm] List<Valore> valori)
    {
        // Validate the input parameters
        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(valoreString) || string.IsNullOrEmpty(voce))
        {
            return BadRequest("Some required fields are missing.");
        }

        // Assuming ApplicationDbContext is injected into the controller via constructor
        try
        {
            // Create a new account instance without the need for the createDto
            var account = new Account
            {
                Nome = nome,
                valoreString = valoreString,
                Valori = valori,
                voce = voce
            };

            // Save the new account to the database
            _context.Accounts.Add(account); // Assuming _context is your DbContext
            await _context.SaveChangesAsync(); // Save the data to the database

            // Return a success response
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account); // Return the created account with a 201 Created status
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., database connection errors)
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    /*  {
  "Nome": "Test Account",
  "valoreString": "Some Value",
  "voce": "Some Voce" --> oggetto da inviare per la post
    } */




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

    /* {
    "nome": "Updated Account Name",
    "valori": [
        {
            "valore": "Updated value",
            "voce": "Updated label"
        }
    ],
    "valoreString": "Updated String Value",
    "voce": "Updated Voice",
    "dataCreazione": "2025-03-05T10:00:00Z"
    } --> oggetto in input sulla put*/




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
