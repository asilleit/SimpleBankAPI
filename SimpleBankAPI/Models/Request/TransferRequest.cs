using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class TransferRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int FromAccount { get; set; }
        [Required]
        public int ToAccount { get; set; }

        public DateTime CreatedAt { get; set; }

        public static Transfer FromTransferRequestToTransfer(TransferRequest transferRequest)
        {
            var transfer = new Transfer
            {
                Id = transferRequest.Id,
                Amount = transferRequest.Amount,
                Fromaccountid = transferRequest.FromAccount,
                Toaccountid = transferRequest.ToAccount,
                CreatedAt = DateTime.Now,
            };
            return transfer;
        }
    }
}

