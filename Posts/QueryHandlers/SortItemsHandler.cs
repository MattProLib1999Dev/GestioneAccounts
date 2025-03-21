using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using MediatR;

public class SortAccountsHandler(IAccountRepository repository) : IRequestHandler<SortAccountsQuery, List<Account>>
{
    private readonly IAccountRepository _repository = repository;

  public async Task<List<Account>> Handle(SortAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _repository.GetAllAccounts();

        // Ordinamento dinamico
        return request.OrderBy switch
        {
            "Nome" => request.Descending ? [.. accounts.OrderByDescending(a => a.Nome)] : accounts.OrderBy(a => a.Nome).ToList(),
            "DataCreazione" => request.Descending ? [.. accounts.OrderByDescending(a => a.Nome)] : [.. accounts.OrderBy(a => a.Nome).ToList(),],
            "ValoreString" => request.Descending ? [.. accounts.OrderByDescending(a => a.valoreString)] : [.. accounts.OrderBy(a => a.valoreString).ToList(),],
            "Voce" => request.Descending ? [.. accounts.OrderByDescending(a => a.voce)] : [.. accounts.OrderBy(a => a.voce).ToList(),],
            _ => [.. accounts] // Se il campo OrderBy non è valido, ritorna la lista non ordinata
        };
    }
}

