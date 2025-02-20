using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore; // Necessario per ToListAsync

public class GetAllValori : IRequest<ICollection<Valori>> {}

public class GetAllValoriHandler : IRequestHandler<GetAllValori, ICollection<Valori>>
{
    private readonly ApplicationDbContext _context;

    public GetAllValoriHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Valori>> Handle(GetAllValori request, CancellationToken cancellationToken)
    {
        // Corretto: usa ToListAsync per ottenere i dati dalla tabella Valori
        return await _context.Valori.ToListAsync(cancellationToken);
    }
}
