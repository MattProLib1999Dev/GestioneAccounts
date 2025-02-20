using GestioneAccounts.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GestioneAccounts.DataAccess.Repositories
{
    public class ValoriRepository(ApplicationDbContext applicationDbContext) : IValoriRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Valori> CreateValori(Valori account)
        {
            _applicationDbContext.Add(account);
            await _applicationDbContext.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteValori(long valoriId)
        {
            var valori = await _applicationDbContext.Valori.FirstOrDefaultAsync(a => a.Id == valoriId);
            if (valori == null) 
                return false;

            _applicationDbContext.Valori.Remove(valori);
            await _applicationDbContext.SaveChangesAsync(); 
            return true;
        }

        public async Task<Valori> GetValoriById(long valoriId)
        {
            return await _applicationDbContext.Valori.FirstOrDefaultAsync(a => a.Id == valoriId) ?? new Valori();
        }


        public async Task<ICollection<Valori>> GetAllValori()
        {
            return await _applicationDbContext.Valori.ToListAsync();
        }

        public async Task<Valori> UpdateValori(Account account, long valoriId)
        {
            var valori = await _applicationDbContext.Valori.FirstOrDefaultAsync(v => v.Id == valoriId);
            
            if (valori == null)
            {
                var valore = new Valori
                {
                    DataCreazione = DateTime.Now
                };
                _applicationDbContext.Valori.Add(valori); 
            }
            else
            {
                valori.Id = (long)account.Id; 
            }

            await _applicationDbContext.SaveChangesAsync();
            return valori; 
        }


    }
}
