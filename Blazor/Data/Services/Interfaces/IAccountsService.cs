using Blazor.Data.Models;
using Blazor.Data.Services.Base;

namespace Blazor.Data.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<(bool, AccountResponse?, string?)>? CreateAccountAsync(CreateAccount account);
        // Task<(bool, AccountDetails?, string?)> GetAccountDetails(int id);
        // Task<(bool, IList<Account>?, string?)> GetAllAccounts();
    }
}