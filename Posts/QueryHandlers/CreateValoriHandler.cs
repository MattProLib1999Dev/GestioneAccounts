using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

public class CreateValoriHandler : IRequestHandler<CreateValoreRequest, Valori>
{
    private readonly ApplicationDbContext _context;

    public CreateValoriHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Valori> Handle(CreateValoreRequest request, CancellationToken cancellationToken)
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
            var valori = new Valori
            {
                AccountId = request.AccountId,
                DataCreazione = DateTime.UtcNow
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
