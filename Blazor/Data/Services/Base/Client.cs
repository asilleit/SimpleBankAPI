using System.Net.Http.Headers;
using System.Runtime.Versioning;
using System.Net.Http;
namespace Blazor.Data.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
    }
}
