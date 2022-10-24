using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBankAPI.Models
{
    public partial class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int AccountId { get; set; }
        public User User { get; set; }
    }
}
