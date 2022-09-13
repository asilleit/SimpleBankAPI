using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;

namespace SimpleBankAPI.Data
{
    public class TransferDb : BaseDb<Transfer>, ITransfersDb
    {
        public TransferDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }
        public async override Task<Transfer> Create(Transfer transfer)
        {
            await _db.AddAsync(transfer);
            await _db.SaveChangesAsync();
            return transfer;
        }

    }
}
