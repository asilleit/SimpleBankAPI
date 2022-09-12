using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class TransferDb : BaseDb<Transfer>, ITransfersDb
    {
        public TransferDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }
    }
}
