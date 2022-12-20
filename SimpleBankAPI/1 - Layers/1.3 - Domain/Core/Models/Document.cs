using System;
using System.Collections.Generic;

namespace SimpleBankAPI.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public byte[] File { get; set; } = null!;
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
