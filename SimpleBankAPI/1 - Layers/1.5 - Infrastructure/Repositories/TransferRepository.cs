using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Application.Repositories
{
    public class TransferRepository : BaseRepository<Transfer>, ITransfersRepository, IBaseRepository<Transfer>
    {
        public TransferRepository(DbContextOptions<postgresContext> options) : base(options)
        {
        }
        public async override Task<Transfer> Create(Transfer transfer)
        {
            await _db.AddAsync(transfer);
            await _db.SaveChangesAsync();
            return transfer;
        }

        public async Task<Transfer> Update(Transfer transferUpdate)
        {
            _db.Transfers.Update(transferUpdate);
            await _db.SaveChangesAsync();
            return transferUpdate;
        }

        public async Task<Transfer> GetById(int transferId)
        {
            return await _db.Transfers.FirstOrDefaultAsync(a => a.Id == transferId);
        }

    }
}
