using MediatR;
using System.Collections.Generic;

namespace GestioneAccounts.Posts.Queries
{
  public record AutocompleteAccountQuery(string Term) : IRequest<List<string>>;
}
