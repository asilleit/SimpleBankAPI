using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBankAPI.Models
{
    public partial class Account
    {
       
        public Account()
        {
            Movements = new HashSet<Movement>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Movement> Movements { get; set; }
    }
}
