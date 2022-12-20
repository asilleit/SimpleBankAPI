using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IMovementsRepository : IBaseRepository<Movement>
    {
        Task<Movement> Create(Movement movementCreate);
    }
}
