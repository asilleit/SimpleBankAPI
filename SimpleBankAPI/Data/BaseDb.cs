using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using static Dapper.SqlMapper;

namespace SimpleBankAPI.Data
{
    public class BaseDb<T> 
    {
        protected postgresContext _db;
        

        public BaseDb(DbContextOptions<postgresContext> options)
        {
            _db = new postgresContext(options);
            
        }
        public async virtual Task<T> Create(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entityToUpdate)
        {
            _db.Update(entityToUpdate);
            await _db.SaveChangesAsync();
            return entityToUpdate;
        }




    }
}
