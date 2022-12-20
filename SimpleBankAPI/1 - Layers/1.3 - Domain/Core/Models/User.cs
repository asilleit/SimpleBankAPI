using System;
using System.Collections.Generic;

namespace SimpleBankAPI.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime PasswordChangedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
