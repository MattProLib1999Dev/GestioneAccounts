using GestioneAccounts.Abstractions;
using GestioneAccounts.DataAccess;
using GestioneAccounts.Posts.Commands;
using MediatR;

namespace GestioneAccounts.Posts.CommandHandlers
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccount, Account>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            if (!request.AccountId.HasValue)
            {
                throw new ArgumentNullException(nameof(request.AccountId), "Account Id cannot be null.");
            }
            if (string.IsNullOrEmpty(request.Nome))
            {
                throw new ArgumentException("Account name cannot be null or empty.", nameof(request.Nome));
            }
            var accountModified = await _accountRepository.UpdateAccount(request.Nome, request.AccountId.Value) ?? throw new KeyNotFoundException("Account not found.");
            return accountModified;
        }

    }
}