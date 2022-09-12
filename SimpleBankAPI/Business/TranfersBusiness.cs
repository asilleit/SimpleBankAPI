using SimpleBankAPI.Models.Request;

namespace SimpleBankAPI.Business
{
    public interface ITransfersBusiness
    {
        Task<string> CreateTransfer(TransferRequest transfer, int userId);
    }
}
