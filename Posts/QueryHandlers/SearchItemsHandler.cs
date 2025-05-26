using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess.Repositories;
using MediatR;

public class SearchAccountQueryHandler : IRequestHandler<SearchAccount, Account>
{
  public AccountRepository _accountRepository { get; set; }
  public SearchAccountQueryHandler(AccountRepository accountRepository)
  {
    _accountRepository = accountRepository;
  }

  // Inietta eventualmente il tuo servizio o contesto dati nel costruttore

  public async Task<Account> Handle(SearchAccount request, CancellationToken cancellationToken)
  {
        // Esegui la logica di ricerca. Esempio:
        var result = await _accountRepository.SearchAccounts(
            nome: request.Nome,
            dataCreazione: request.DataCreazione,
            valoreString: request.ValoreString,
            voce: null // Puoi passare un valore specifico se necessario
        );

        // Restituisci il risultato della ricerca
        return result.FirstOrDefault() ?? new Account();
  }

}
