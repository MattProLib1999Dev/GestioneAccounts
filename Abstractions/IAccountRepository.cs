using GestioneAccounts.DataAccess;
using matteo.proietti.progetti.Progetti_personali_e_esercitazioni.esercitazioni_dotnet.GestioneAccounts.BE.Domain.Models;

namespace GestioneAccounts.Abstractions
{
	public interface IAccountRepository
	{
		Task<ICollection<DataAccess.Account>> GetAllAccounts();
		Task<DataAccess.Account> GetAccountById(long accountId);
        Task<DataAccess.Account> CreateAccount(DataAccess.Account account);
        Task<DataAccess.Account> UpdateAccount(string ?nome, long accountId);
		Task<bool> DeleteAccount(long accountId);

	}
}