using System.Net.Http.Headers;
using System.Runtime.Versioning;
using System.Net.Http;

namespace Blazor.Data.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
