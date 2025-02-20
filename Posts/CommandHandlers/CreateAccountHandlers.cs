using GestioneAccounts.Abstractions;
using GestioneAccounts.DataAccess;
using GestioneAccounts.Posts.Commands;
using MediatR;

namespace GestioneAccounts.Posts.CommandHandlers
{
    public class CreateAccountHandlers(IAccountRepository accountRepository) : IRequestHandler<CreateAccount, Account>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<Account> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Nome = request.Nome,
            };
            return await _accountRepository.CreateAccount(account);
        }
    }
}