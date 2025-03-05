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
    public async Task<IActionResult> Create([FromBody] Account createDto)
    {
      if (createDto == null)
      {
        return BadRequest("createDto is required.");
      }

      // Validate the createDto if needed, for example checking if required fields are not empty
      if (string.IsNullOrEmpty(createDto.Nome) || string.IsNullOrEmpty(createDto.valoreString) || string.IsNullOrEmpty(createDto.voce))
      {
        return BadRequest("Some required fields are missing.");
      }

      // Assuming ApplicationDbContext is injected into the controller via constructor
      try
      {
        // Save the new account to the database
        _context.Accounts.Add(createDto); // Assuming _context is your DbContext
        await _context.SaveChangesAsync(); // Save the data to the database

        // Return a success response
        return CreatedAtAction(nameof(GetById), new { id = createDto.Id }, createDto); // Return the created account with a 201 Created status
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
