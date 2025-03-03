using GestioneAccounts.DataAccess;
<<<<<<< HEAD
=======
using matteo.proietti.progetti.Progetti_personali_e_esercitazioni.esercitazioni_dotnet.GestioneAccounts.BE.Domain.Models;
>>>>>>> origin/main

namespace GestioneAccounts.Abstractions
{
	public interface IAccountRepository
	{
<<<<<<< HEAD
		Task<ICollection<Account>> GetAllAccounts();
		Task<Account> GetAccountById(long accountId);
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(string ?nome, long accountId);
=======
		Task<ICollection<DataAccess.Account>> GetAllAccounts();
		Task<DataAccess.Account> GetAccountById(long accountId);
        Task<DataAccess.Account> CreateAccount(DataAccess.Account account);
        Task<DataAccess.Account> UpdateAccount(string ?nome, long accountId);
>>>>>>> origin/main
		Task<bool> DeleteAccount(long accountId);

	}
}