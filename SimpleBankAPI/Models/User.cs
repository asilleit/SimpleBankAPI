using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public int Id { get; set; }
        
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
