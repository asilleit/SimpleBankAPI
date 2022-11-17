using SimpleBankAPI.Interfaces;
using System.Data;

namespace SimpleBankAPI.Application.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        public IUsersRepository UserRepository { get; }
        public IAccountsRepository AccountRepository { get; }
        public ITransfersRepository TransferRepository { get; }
        public IDocumentsRepository DocumentsRepository { get; }

        private readonly IDbTransaction _dbTransaction;

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
