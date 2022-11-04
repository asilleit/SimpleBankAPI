using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface ITransfersRepository : IBaseRepository<Transfer>
    {
        Task<Transfer> Create(Transfer transferCreate);
        Task<Transfer> Update(Transfer transferUpdate);
        Task<Transfer> GetById(int userId);
    }
}
