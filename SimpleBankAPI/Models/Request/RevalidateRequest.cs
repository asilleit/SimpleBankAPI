
using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class RevalidateRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
