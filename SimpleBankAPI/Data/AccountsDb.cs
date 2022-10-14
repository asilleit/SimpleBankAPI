using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;


namespace SimpleBankAPI.Data
{
    public class AccountsDb : BaseDb<Account>, ITokenDb, IBaseDb<Token>
    {

        public AccountsDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }
        public async override Task<Account> Create(Account account)
        {
            await _db.AddAsync(account);
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            return await _db.Accounts.Where(a => a.UserId == userId).ToListAsync();

        }

        public async Task<Account> GetById(int accountId)
        {
            return await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<Account> Update(Account accountUpdate)
        {
             _db.Accounts.Update(accountUpdate);
            await _db.SaveChangesAsync();
            return accountUpdate;
        }
    }
}
