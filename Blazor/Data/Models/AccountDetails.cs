using Blazor.Data.Services.Base;

namespace Blazor.Data.Models
{
    public class AccountDetails
    {
        public AccountResponse Account { get; set; }
        public List<Movim> Movims { get; set; }
    }
}
