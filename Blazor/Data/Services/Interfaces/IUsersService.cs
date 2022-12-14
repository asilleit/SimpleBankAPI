using Blazor.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Blazor.Data.Services.Base;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blazor.Data.Services.Interfaces
{
    public interface IUsersService
    {
        Task<(bool, LoginUserResponse?, string?)> LoginAsync(User user);
        Task<(bool, string?)> PostUserAsync(CreateUser user);
    }
}
