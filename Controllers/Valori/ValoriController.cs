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
    public class ValoriController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        // Constructor for Dependency Injection
        public ValoriController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: Get all valori
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var valori = await _mediator.Send(new GetAllValori());
            return Ok(valori);
        }


        // POST: Create a new Valori
        [HttpPost("Create")]
    public async Task<IActionResult> Create(
            [FromForm] string valoreString,
            [FromForm] string voce,
            [FromForm] int accountId)
    {
      // Verifica che i parametri non siano nulli o vuoti
      if (string.IsNullOrEmpty(valoreString) || string.IsNullOrEmpty(voce) || accountId <= 0)
      {
        return BadRequest("Invalid input data.");
      }

      // Mappare i dati dal parametro in un'istanza della classe Valore
      var createValoriInstance = new Valore
      {
        AccountId = accountId,
        ValoreStr = valoreString,
        Voce = voce
      };

      // Verifica che il modello sia valido
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        // Aggiungere alla base dati
        _context.Valori.Add(createValoriInstance);
        await _context.SaveChangesAsync();

        // Restituire la risposta di creazione con l'header Location
        return CreatedAtAction(nameof(GetById), new { id = createValoriInstance.Id }, createValoriInstance);
      }
      catch (Exception ex)
      {
        // Gestire errori imprevisti
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }

      /*   {
          "AccountId": 1,
          "Nome": "Some Name",
          "valoreString": "Some Value",
          "voce": "Some Voce" --> oggetto da inviare come post
        } */



        // GET: Get Valori by ID
        // GET: Retrieve Valori by ID
[HttpGet("{id}")]
public async Task<IActionResult> GetById(long id)
{
    var valori = await _context.Valori.FindAsync(id);

    if (valori == null)
    {
        return NotFound(new { message = $"Valori with ID {id} not found." });
    }

    return Ok(valori);
}

// PUT: Edit Valori by ID
[HttpPut("{id}")]
public async Task<IActionResult> Edit(long id, [FromBody] Valori valori)
{
    if (id != valori.Id)
    {
        return BadRequest(new { message = "ID mismatch. The provided ID does not match the values' ID." });
    }

    // Validate the model
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        // Find the existing record to update
        var existingValori = await _context.Valori.FindAsync(id);
        if (existingValori == null)
        {
            return NotFound(new { message = $"Valori with ID {id} not found for update." });
        }

        // Update the properties
        existingValori.ValoreStr = valori.valoreString;
        existingValori.Voce = valori.voce;

        // Save the changes
        await _context.SaveChangesAsync();

        return Ok(existingValori);
    }
    catch (Exception ex)
    {
        // Handle any errors during the update operation
        return StatusCode(500, new { message = $"An error occurred while updating the Valori: {ex.Message}" });
    }
}

// DELETE: Delete Valori by ID
[HttpDelete("Delete/{id}")]
public async Task<IActionResult> Delete(long id)
{
    try
    {
        // Find the existing record to delete
        var valoriToDelete = await _context.Valori.FindAsync(id);
        if (valoriToDelete == null)
        {
            return NotFound(new { message = $"Valori with ID {id} not found for deletion." });
        }

        // Remove the record
        _context.Valori.Remove(valoriToDelete);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Valori deleted successfully." });
    }
    catch (Exception ex)
    {
        // Handle any errors during the delete operation
        return StatusCode(500, new { message = $"An error occurred while deleting the Valori: {ex.Message}" });
    }
}

    }
}
