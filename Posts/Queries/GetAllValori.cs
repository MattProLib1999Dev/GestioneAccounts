using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore; // Necessario per ToListAsync

// Request class
public class GetAllValori : IRequest<ICollection<Valori>> {}

// Handler class
public class GetAllValoriHandler(ApplicationDbContext context) : IRequestHandler<GetAllValori, ICollection<Valori>>
{
    private readonly ApplicationDbContext _context = context;

  public async Task<ICollection<Valori>> Handle(GetAllValori request, CancellationToken cancellationToken)
    {
        // Asynchronously retrieve all records from the "Valori" table
        return (ICollection<Valori>)await _context.Valori.ToListAsync(cancellationToken);
    }
}
