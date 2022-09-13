using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersDb 
    {
        Task<Transfer> Create(Transfer user);
    }
}
