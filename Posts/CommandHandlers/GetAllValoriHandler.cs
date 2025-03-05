using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllValoriHandler(ApplicationDbContext context) : IRequestHandler<GetAllValori, ICollection<Valore>>
{
    private readonly ApplicationDbContext _context = context;

  public async Task<ICollection<Valore>> Handle(GetAllValori request, CancellationToken cancellationToken)
    {
        // Restituisce tutti i valori dal database
        return await _context.Valori.ToListAsync(cancellationToken);
    }
}
