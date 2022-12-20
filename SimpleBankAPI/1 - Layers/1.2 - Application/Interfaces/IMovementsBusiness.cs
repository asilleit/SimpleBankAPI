using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IMovementsBusiness
    {
        Task<Movement> Create(int accountId, decimal amount);
    }
}