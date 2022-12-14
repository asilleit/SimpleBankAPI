using Blazor.Data.Services.Base;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; }
    }
}
