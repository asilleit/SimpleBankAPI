using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBankAPI.Models
{
    public partial class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        [MinLength(3), DefaultValue("EUR")]
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
