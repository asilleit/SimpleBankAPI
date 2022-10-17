using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersDb : IBaseDb<Transfer>
    {
        Task<Transfer> Create(Transfer transferCreate);
        Task<Transfer> Update(Transfer transferUpdate);
        Task<Transfer> GetById(int userId);
    }
}
