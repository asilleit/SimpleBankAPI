using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Blazor.Data.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
