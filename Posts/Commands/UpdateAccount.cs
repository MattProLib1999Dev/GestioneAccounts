using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;

namespace GestioneAccounts.Posts.Commands
{
	public class UpdateAccount: IRequest<Account>
	{
        public long ?AccountId { get; set; }
        public string? Nome { get; set; }
	}
}
