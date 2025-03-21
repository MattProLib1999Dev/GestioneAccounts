using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SearchAccountsHandler(IAccountRepository repository) : IRequestHandler<SearchItemsQuery, List<Account>>
{
    private readonly IAccountRepository _repository = repository;

  public async Task<List<Account>> Handle(SearchItemsQuery request, CancellationToken cancellationToken)
    {
        return (List<Account>)await _repository.SearchAccounts(request.Nome, request.DataCreazione, request.ValoreString, request.Voce);
    }
}
