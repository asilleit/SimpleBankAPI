using System.Data;

namespace SimpleBankAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IUsersRepository UserRepository { get; }
        IAccountsRepository AccountRepository { get; }
        ITransfersRepository TransferRepository { get; }
        //ITransferRepository transferRepository { get; }
        //Guid Id { get; }
        //IDbConnection Connection { get; }
        //IDbTransaction Transaction { get; }
        IDbTransaction Begin();
        void Commit();
        void Rollback();
    }
}
