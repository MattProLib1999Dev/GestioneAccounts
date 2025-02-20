using GestioneAccounts.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GestioneAccounts.DataAccess.Repositories
{
    public class AccountRepository(ApplicationDbContext applicationDbContext) : IAccountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Account> CreateAccount(Account account)
        {
            _applicationDbContext.Add(account);
            await _applicationDbContext.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAccount(long accountId)
        {
            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null) 
                return false;

            _applicationDbContext.Accounts.Remove(account);
            await _applicationDbContext.SaveChangesAsync(); 
            return true;
        }

        public async Task<Account> GetAccountById(long accountId)
        {
            return await _applicationDbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId) ?? new Account();
        }


        public async Task<ICollection<Account>> GetAllAccounts()
        {
            return await _applicationDbContext.Accounts.ToListAsync();
        }

        public async Task<Account> UpdateAccount(string? nome, long accountId)
        {
            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
                return new Account { Id = accountId, Nome = nome ?? "Default" }; 

            account.Nome = nome ?? account.Nome;
            await _applicationDbContext.SaveChangesAsync();
            return account;
        }

    }
}
