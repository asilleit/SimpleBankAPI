using SimpleBankAPI.Infrastructure.Kafka;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using System.Security.Authentication;
using System.Transactions;

namespace SimpleBankAPI.Business
{
    public class TransfersBusiness : ITransfersBusiness
    {
        protected ITransfersRepository _transfersDb;
        protected ICommunicationsBusiness _communicationsBusiness;

        protected IMovementsBusiness _movementsBusiness;
        protected IAccountsRepository _accountsDb;

        public TransfersBusiness(IMovementsBusiness movementsBusiness, ITransfersRepository transfersDb, IAccountsRepository accountsRepository, ICommunicationsBusiness communicationsBusiness)
        {
            _movementsBusiness = movementsBusiness;
            _transfersDb = transfersDb;
            _accountsDb = accountsRepository;
            _communicationsBusiness = communicationsBusiness;
        }
        public async Task<string> Create(TransferRequest transferRequest, int userId)
        {
            // using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            // {
            // Colar um try Catch e retornar exceção controlada para o controller
            //Convert TransferRequest to Transfer
            var transfer = TransferRequest.FromTransferRequestToTransfer(transferRequest);
            var fromAccount = await _accountsDb.GetById(transfer.Fromaccountid);
            var toAccount = await _accountsDb.GetById(transfer.Toaccountid);

            //switch
            //Validates
            if (fromAccount.UserId != userId) throw new AuthenticationException("User don't owner account");
            if (fromAccount is null || toAccount is null) throw new ArgumentException("Accounts not valid");
            if (fromAccount.Id == toAccount.Id) throw new ArgumentException("Accounts not valid, because is the same");
            if (fromAccount.Balance < transfer.Amount) throw new ArgumentException("Insufficient funds from your account");
            if (fromAccount.Currency != toAccount.Currency) throw new ArgumentException("Currency isn't the same");

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //create debit movement
                    var amountToCredit = transfer.Amount * -1;
                    await _movementsBusiness.Create(transfer.Fromaccountid, amountToCredit);

                    //create credit movement
                    await _movementsBusiness.Create(transfer.Toaccountid, transfer.Amount);

                    await _transfersDb.Create(transfer);

                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "transfer completed";
            // }



        }
    }
}
