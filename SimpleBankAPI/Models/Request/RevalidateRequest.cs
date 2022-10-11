using Microsoft.Build.Framework;

namespace SimpleBankAPI.Models.Request
{
    public class RevalidateRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
