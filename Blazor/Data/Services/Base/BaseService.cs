using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
namespace Blazor.Data.Services.Base
{
    public class BaseService
    {
        protected IClient _client;
        protected ProtectedLocalStorage _protectedLocalStorage;

        public BaseService(IClient client, ProtectedLocalStorage protectedLocalStorage)
        {
            _client = client;
            _protectedLocalStorage = protectedLocalStorage;
        }
        protected  (string, string) HandleApiException(ApiException ex)
        {
            if(ex.Headers != null)
            { 
                if(ex.Headers.Any(h => h.Value.Any(v => v.Contains("token expired"))))
                {
                    return (ex.Response, "token expired");
                }
            }
            return (ex.Response, ex.StatusCode.ToString());
        }
    }
}
