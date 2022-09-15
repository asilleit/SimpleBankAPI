using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using static Dapper.SqlMapper;

namespace SimpleBankAPI.Data
{
    public class AccountsDb : BaseDb<Account>, IAccountsDb
    {
        protected postgresContext _postgres;
        public AccountsDb(DbContextOptions<postgresContext> options) : base(options)
        {
            _postgres = new postgresContext(options);
        }
        public async override Task<Account> Create(Account account)
        {
            await _postgres.AddAsync(account);
            await _postgres.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            return await _postgres.Accounts.Where(a => a.UserId == userId).ToListAsync();

        }

        public async Task<Account> GetById(int accountId)
        {
            return await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<Account> Update(Account accountUpdate)
        {
            _postgres.Update(accountUpdate);
            await _postgres.SaveChangesAsync();
            return accountUpdate;
        }
    }
}
