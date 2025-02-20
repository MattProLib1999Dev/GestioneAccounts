using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestioneAccounts.DataAccess;
using MediatR;
using GestioneAccounts.Posts.Queries;
using GestioneAccounts.Posts.Commands;
using System.ComponentModel.DataAnnotations;

namespace GestioneAccounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ValoriController(ApplicationDbContext context, IMediator mediator) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMediator _mediator = mediator;

        // GET: Account
         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var valori = await _mediator.Send(new GetAllValori());
            return Ok(valori);
        }

        // POST: Account/Create
       

            // POST per creare un nuovo Valori
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Valori createValori)
        {
            if (createValori == null)
            {
                return BadRequest("Invalid valori data.");
            }

            // Non assegnare manualmente l'ID, lascialo vuoto.
            var createValoriInstance = new Valori
            {
                AccountId = createValori.AccountId,   // Usa l'AccountId fornito
                Nome = createValori.Nome,             // Nome del valore
                DataCreazione = createValori.DataCreazione // Data di creazione
            };

            // Verifica se il modello è valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Salva nel database senza modificare l'ID, il database lo assegnerà automaticamente
            _context.Valori.Add(createValoriInstance);
            await _context.SaveChangesAsync();

            // Restituisci il risultato
            return CreatedAtAction(nameof(GetById), new { id = createValoriInstance.Id }, createValoriInstance);
        }



        // GET per ottenere un Valori per ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Valori>> GetById(long id)
        {
            var valori = await _context.Valori.FindAsync(id);

            if (valori == null)
            {
                return NotFound();
            }

            return Ok(valori);
        }

        
    



        // GET: Account/Edit/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetValoriById(long id)
        {
            var getValori = new GetValoriById { Id = id };
            var valori = await _mediator.Send(getValori);
            if (valori == null)
            {
                return NotFound();
            }
            return Ok(valori);
        }
        // PUT: Account/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,Nome")] Valori valori)
        {
            // Check if the id from the route matches the account id
            if (id != valori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateValori = new UpdateValori
                {
                    Nome = valori.Nome
                };
                var updatedValori = await _mediator.Send(updateValori);
                if (updatedValori != null)
                {
                    return Ok(updatedValori);
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
