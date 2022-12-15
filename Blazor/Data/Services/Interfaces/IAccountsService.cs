using Blazor.Data.Models;
using Blazor.Data.Services.Base;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blazor.Data.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<(bool, AccountResponse?, string?)>? PostAccountAsync(CreateAccount account);
        Task<(bool, IList<AccountResponse>?, string?)> GetAllAccounts();
        Task<(bool, AccountDetails?, string?)> GetAccountDetails(int id);
    }
}