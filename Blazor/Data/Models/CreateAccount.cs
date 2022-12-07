using System.ComponentModel.DataAnnotations;


namespace Blazor.Data.Models
{
    public class CreateAccount
    {
        public int Amount { get; set; }
        public string Currency { get; set; }

    }
}
