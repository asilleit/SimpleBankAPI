using System;
using System.Collections.Generic;

namespace SimpleBankAPI.Models
{
    public partial class Movement
    {
        public int Id { get; set; }
        public int Accountid { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
