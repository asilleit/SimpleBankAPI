namespace SimpleBankAPI.Models.Response
{
    public class TransferResponse
    {
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public static TransferResponse ResponseToTransfer(TransferResponse movement)
        {
            var movim = new TransferResponse
            {
                Amount = movement.Amount,
                CreatedAt = movement.CreatedAt,
            };
            return movim;
        }


    }
}
