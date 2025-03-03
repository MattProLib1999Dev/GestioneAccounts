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
        public async Task<IActionResult> Create([FromBody] Valori createValori)
        {
            if (createValori == null)
            {
                return BadRequest("Invalid valori data.");
            }

            // Map Valori model (DTO) to Valore entity
            var createValoriInstance = new Valore
            {
                AccountId = createValori.AccountId, // Use the provided AccountId
                ValoreStr = createValori.valoreString,  // Ensure property name matches your model's
                Voce = createValori.voce  // Ensure property name matches your model's
            };

            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Save to database
                _context.Valori.Add(createValoriInstance);
                await _context.SaveChangesAsync();

                // Return the created response with a Location header to the new resource
                return CreatedAtAction(nameof(GetById), new { id = createValoriInstance.Id }, createValoriInstance);
            }
            catch (Exception ex)
            {
                // Log the error (if logging is configured)
                // Handle any exceptions during the save operation
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
        existingValori.ValoreString = valori.valoreString;
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
