using SimpleBankAPI.Models.Request;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersBusiness
    {
        Task<string> Create(TransferRequest transfer, int userId);

    }
}
