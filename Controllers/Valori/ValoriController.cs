using Microsoft.AspNetCore.Mvc;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.BE.Domain.Models;

namespace GestioneAccounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValoriController(ILogger<ValoriController> logger, ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        private readonly ILogger<ValoriController> _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger is not being injected!");

    // GET: Get all valori
    [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var valori = await _mediator.Send(new GetAllValori());
                return Ok(valori);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST: Create a new Valori
        [HttpPost("create")]
        public async Task<IActionResult> CreateValori([FromBody] Valori request)
        {
            // Controllo dei parametri ricevuti
            if (request == null || string.IsNullOrWhiteSpace(request.Nome) ||
                string.IsNullOrWhiteSpace(request.voce))
            {
                _logger.LogWarning("Invalid request: missing required fields (Nome, voce).");
                return BadRequest("Invalid request. Nome and voce are required.");
            }

            try
            {
              // Log della richiesta di creazione
              await Task.Run(() => _logger.LogInformation("Creating valori for Nome: {Nome}, Voce: {Voce}", request.Nome, request.voce));

              // Aggiungere la logica per creare il valore nel database o nel sistema di persistenza
              // Ad esempio: await _valoriService.CreateValoriAsync(request);

              // Risposta con l'oggetto appena creato
              return CreatedAtAction(nameof(CreateValori), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                // Log degli errori
                _logger.LogError(ex, "An unexpected error occurred while creating valori.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request. Please try again later.");
            }
        }

        // GET: Get Valori by ID
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
        [HttpDelete("{id}")]
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
