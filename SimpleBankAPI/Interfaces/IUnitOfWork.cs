using System.Data;

namespace SimpleBankAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IUsersDb UserRepository { get; }
        IAccountsDb AccountRepository { get; }
        ITransfersDb TransferRepository { get; }
        //ITransferRepository transferRepository { get; }
        //Guid Id { get; }
        //IDbConnection Connection { get; }
        //IDbTransaction Transaction { get; }
        IDbTransaction Begin();
        void Commit();
        void Rollback();
    }
}
