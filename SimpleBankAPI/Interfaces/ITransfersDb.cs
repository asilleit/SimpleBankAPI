using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersDb : IBaseDb<Transfer>
    {
        Task<Transfer> Create(Transfer user);
    }
}
