using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class UsersDb : BaseDb<User>, IUsersDb
    {
        public UsersDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }

        public async override Task<User> Create(User user)
        {
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUsername(string userName)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<User> GetById(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

    }
}
