using Blazor.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Blazor.Data.Services.Base;
using System.Threading.Tasks;

namespace Blazor.Data.Services.Interfaces
{
    public interface IUsersService
    {
        Task<LoginUserResponse> LoginAsync(User user);
    }
}
