using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models
{
    public class CreateUser
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]

        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]

        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]

        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]

        public string FullName { get; set; }

    }
}
