using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Application.Repositories
{
    public class MovementsRepository :  BaseRepository<Movement>, IMovementsRepository, IBaseRepository<Movement>
    {
        public MovementsRepository(DbContextOptions<postgresContext> options) : base(options)
        {
        }

        public async override Task<Movement> Create(Movement movement)
        {
            await _db.AddAsync(movement);
            await _db.SaveChangesAsync();
            return movement;
        }

    }
}
