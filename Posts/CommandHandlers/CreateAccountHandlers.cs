using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Posts.Commands;
using MediatR;

namespace GestioneAccounts.Posts.CommandHandlers;
public class CreateAccountHandlers(IAccountRepository accountRepository) : IRequestHandler<CreateAccount, Account>
{
  private readonly IAccountRepository _accountRepository = accountRepository;

  public async Task<Account> Handle(CreateAccount request, CancellationToken cancellationToken)
  {
      var listValori = new List<Valore>();
      var account = new Account
      {
        Nome = request.Nome,
        Valori = listValori
      };
      return await _accountRepository.CreateAccount(account);
  }
}
