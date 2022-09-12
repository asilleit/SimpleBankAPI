using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models
{
    public partial class Transfer
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Fromaccountid { get; set; }
        public int Toaccountid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
