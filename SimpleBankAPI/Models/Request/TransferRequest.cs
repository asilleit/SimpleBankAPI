using SimpleBankAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class TransferRequest
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int FromAccount { get; set; }
        [Required]
        public int ToAccount { get; set; }


        public static Transfer FromTransferRequestToTransfer(TransferRequest transferRequest)
        {
            var transfer = new Transfer
            {
                Amount = transferRequest.Amount,
                Fromaccountid = transferRequest.FromAccount,
                Toaccountid = transferRequest.ToAccount
            };
            return transfer;
        }
    }
}

