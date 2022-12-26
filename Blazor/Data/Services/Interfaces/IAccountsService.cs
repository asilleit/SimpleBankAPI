using Blazor.Data.Models;
using Blazor.Data.Services.Base;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.Data.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<(bool, AccountResponse?, string?)>? PostAccountAsync(CreateAccount account);
        Task<(bool, IList<AccountResponse>?, string?)> GetAllAccounts();
        Task<(bool, GetAccountResponse?, string?)> GetAccountDetails(int id);
        Task<(bool, string?, string?)> PostDocumentAsync(int id, IBrowserFile file);
        Task<(bool, DocumentResponse?, string?)> DownloadDocumentfromAccountAsync(int id);
        Task<(bool, ICollection<DocumentResponse>?, string?)> GetDoccumentAccountAsync(int id);

    }
}