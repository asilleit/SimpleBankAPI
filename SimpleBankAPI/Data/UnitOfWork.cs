using SimpleBankAPI.Interfaces;
using System.Data;

namespace SimpleBankAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        public IUsersDb UserRepository { get; }
        public IAccountsDb AccountRepository { get; }
        public ITransfersDb TransferRepository { get; }

        private readonly IDbTransaction _dbTransaction;

        public UnitOfWork(IUsersDb usersDb, IAccountsDb accountsDb, ITransfersDb transfersDb, IDbTransaction dbTransaction)
        {
            UserRepository = usersDb;
            AccountRepository = accountsDb;
            TransferRepository = transfersDb;
            _dbTransaction = dbTransaction;
        }

        public IDbTransaction Begin()
        {
            try
            {
                return _dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                return _dbTransaction;
            }
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                //Dispose();
                //_dbTransaction.Connection.BeginTransaction();

            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                _dbTransaction.Connection?.Close();
                _dbTransaction.Connection?.Dispose();
                _dbTransaction.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            Dispose();
        }
    }
}
