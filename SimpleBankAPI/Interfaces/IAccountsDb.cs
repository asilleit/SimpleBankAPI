using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IAccountsDb 
    {
        Task<Account> Create(Account entity);
        Task<List<Account>> GetAccountsByUser(int userId);
        Task<Account> GetById(int userId);
    }
}
