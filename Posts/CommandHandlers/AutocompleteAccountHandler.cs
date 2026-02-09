using MediatR;
using GestioneAccounts.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestioneAccounts.Posts.Queries
{
  public class AutocompleteAccountHandler : IRequestHandler<AutocompleteAccountQuery, List<string>>
  {
    private readonly ApplicationDbContext _context;

    public AutocompleteAccountHandler(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<string>> Handle(AutocompleteAccountQuery request, CancellationToken cancellationToken)
    {
      var term = request.Term?.Trim().ToLower();

      if (string.IsNullOrWhiteSpace(term) || term.Length < 3)
        return new List<string>();

      var results = await _context.Accounts
        .Where(a => a.UserName.ToLower().Contains(term) || a.Nome.ToLower().Contains(term))
        .Select(a => a.UserName)
        .Take(20)
        .ToListAsync(cancellationToken);

      return results;
    }
  }
}
