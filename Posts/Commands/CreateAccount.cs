using GestioneAccounts.DataAccess;
using MediatR;

namespace GestioneAccounts.Posts.Commands
{
	public class CreateAccount: IRequest<Account>
	{
        public string? Nome { get; set; } = String.Empty;
	}
}