using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> Create(User user);
        Task<User> GetByUsername(string userName);
        Task<User> GetById(int userId);
    }
}
