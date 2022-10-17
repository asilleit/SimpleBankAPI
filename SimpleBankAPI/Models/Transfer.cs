using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBankAPI.Models
{
    public partial class Transfer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Fromaccountid { get; set; }
        public int Toaccountid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
