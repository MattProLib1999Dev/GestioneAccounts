using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;

namespace GestioneAccounts.Posts.Queries
{
	public class GetAccountById: IRequest<Account>
	{
		public long? Id { get; set; }
	}
}
