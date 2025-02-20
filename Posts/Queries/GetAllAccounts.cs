using GestioneAccounts.DataAccess;
using MediatR;

namespace GestioneAccounts.Posts.Queries
{
	public class GetAllAccounts: IRequest<ICollection<Account>>
	{
        
	}
}