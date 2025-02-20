using GestioneAccounts.DataAccess.Repositories;
using GestioneAccounts.Posts.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestioneAccounts.Posts.CommandHandlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccount, Unit>
    {
        private readonly AccountRepository _accountRepository;

        public DeleteAccountHandler(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Unit> Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
            }, cancellationToken);
            
            return Unit.Value;
        }

    }
}
