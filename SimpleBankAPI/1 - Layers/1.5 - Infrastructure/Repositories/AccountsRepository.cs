using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;


namespace SimpleBankAPI.Application.Repositories
{
    public class AccountsRepository : BaseRepository<Account>, IAccountsRepository, IBaseRepository<Account>
    {

        public AccountsRepository(DbContextOptions<postgresContext> options) : base(options)
        {
        }
        public async Task<Account> Create(Account account)
        {
            await _db.AddAsync(account);
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            return await _db.Accounts.Where(a => a.UserId == userId).ToListAsync();

        }

        public async Task<Account> GetById(int id)
        {
            // return await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            return await _db.Accounts.Include(a => a.Movements).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> Update(Account accountUpdate)
        {
            _db.Accounts.Update(accountUpdate);
            await _db.SaveChangesAsync();
            return accountUpdate;
        }
    }
}
