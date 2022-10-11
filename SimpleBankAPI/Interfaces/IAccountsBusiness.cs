using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IAccountsBusiness
    {
        Task<AccountResponse> Create(AccountRequest accontm, int userId);
        Task<List<Account>> GetAccountsByUser(int userId);
        Task<Account> GetById(int accountId);
        void Update(Account accountToUpdate);
    }
}
