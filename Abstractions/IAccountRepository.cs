using GestioneAccounts.DataAccess;

namespace GestioneAccounts.Abstractions
{
	public interface IAccountRepository
	{
		Task<ICollection<Account>> GetAllAccounts();
		Task<Account> GetAccountById(long accountId);
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(string ?nome, long accountId);
		Task<bool> DeleteAccount(long accountId);

	}
}