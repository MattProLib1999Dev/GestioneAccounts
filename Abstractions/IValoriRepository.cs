using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;

namespace GestioneAccounts.Abstractions
{
	public interface IValoriRepository
	{
        Task<ICollection<Valori>> GetAllValori();
		Task<Valori> GetValoriById(long valoriId);
        Task<Valori> CreateValori(Valori account);
        Task<Valori> UpdateValori(Account account, long valoriId);
		Task<bool> DeleteValori(long valoriId);

	}
}
