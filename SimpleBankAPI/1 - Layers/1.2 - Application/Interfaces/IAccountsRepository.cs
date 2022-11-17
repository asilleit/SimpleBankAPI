using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IAccountsRepository : IBaseRepository<Account>
    {
        Task<Account> Create(Account accountCreate);
        Task<List<Account>> GetAccountsByUser(int userId);
        Task<Account> GetById(int userId);
        Task<Account> Update(Account accountUpdate);
    }
}
