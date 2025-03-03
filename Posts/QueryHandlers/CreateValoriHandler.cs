using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CreateValoriHandler(ApplicationDbContext context) : IRequest<Valore>
{
  private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

  public async Task<Valore> Handle(CreateValoreRequest request, CancellationToken cancellationToken)
    {
        if (request.AccountId <= 0)
        {
            throw new ArgumentException("AccountId non valido.", nameof(request.AccountId));
        }

        // Verifica se l'AccountId esiste nel database
        var account = await _context.Accounts
            .AsNoTracking() // Ottimizzazione se non sono necessarie modifiche all'oggetto Account
            .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);

        if (account == null)
        {
            throw new InvalidOperationException($"L'account con ID {request.AccountId} non esiste.");
        }

        try
        {
            // Crea il nuovo valore
            var valori = new Valore
            {
                AccountId = request.AccountId,
                DataCreazione = DateTime.UtcNow,
                Nome = account.Nome,
                ValoreStr = account.valoreString,
                Voce = account.voce
            };

            // Aggiungi il nuovo record nella tabella Valori
            await _context.Valori.AddAsync(valori, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Restituisci il valore creato
            return valori;
        }
        catch (Exception ex)
        {
            // Log dell'errore (idealmente usi una libreria di logging come Serilog, NLog, ecc.)
            Console.Error.WriteLine($"Errore durante il salvataggio: {ex.Message}");
            throw new InvalidOperationException("Si è verificato un errore durante il salvataggio del valore.", ex);
        }
    }
}
