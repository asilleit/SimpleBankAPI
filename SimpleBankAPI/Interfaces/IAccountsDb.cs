using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IAccountsDb : IBaseDb<Account>
    {
        Task<Account> Create(Account accountCreate);
        Task<List<Account>> GetAccountsByUser(int userId);
        Task<Account> GetById(int userId);
        Task<Account> Update(Account accountUpdate);
    }
}
