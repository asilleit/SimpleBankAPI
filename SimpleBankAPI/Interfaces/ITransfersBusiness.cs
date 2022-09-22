using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersBusiness
    {
        Task<string> Create(TransferRequest transfer, int id);

    }
}
