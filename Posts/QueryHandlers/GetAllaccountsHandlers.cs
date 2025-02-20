using GestioneAccounts.Abstractions;
using GestioneAccounts.DataAccess;
using GestioneAccounts.Posts.Queries;
using MediatR;

namespace GestioneAccounts.Posts.QueryHandlers
{
    public class GetAllaccountsHandlers : IRequestHandler<GetAllAccounts, ICollection<Account>>
    {
        public readonly IAccountRepository _accountRepository;
        public GetAllaccountsHandlers(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<ICollection<Account>> Handle(GetAllAccounts request, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAllAccounts();
        }
    }
}