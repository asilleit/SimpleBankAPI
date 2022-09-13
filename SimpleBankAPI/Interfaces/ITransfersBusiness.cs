using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersBusiness
    {
        Task<TransferResponse> Create(TransferRequest transfer);

    }
}
