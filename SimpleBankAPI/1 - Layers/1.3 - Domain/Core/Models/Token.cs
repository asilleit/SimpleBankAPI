using System;
using System.Collections.Generic;

namespace SimpleBankAPI.Models
{
    public partial class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
