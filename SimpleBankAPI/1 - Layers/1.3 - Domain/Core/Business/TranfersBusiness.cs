using SimpleBankAPI.Infrastructure.Kafka;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using System.Security.Authentication;
using System.Transactions;

namespace SimpleBankAPI.Business
{
    public class TransfersBusiness : ITransfersBusiness
    {
        private readonly ITransfersRepository _transfersDb;
        private readonly ICommunicationsBusiness _communicationsBusiness;
        private readonly IAccountsRepository _accountsDb;

        public TransfersBusiness(ITransfersRepository transfersDb, IAccountsRepository accountsRepository,ICommunicationsBusiness communicationsBusiness)
        {
            _transfersDb = transfersDb;
            _accountsDb = accountsRepository;
            _communicationsBusiness= communicationsBusiness;
        }
        public async Task<string> Create(TransferRequest transferRequest, int userId)
        {

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                //Convert TransferRequest to Transfer
                var transfer = TransferRequest.FromTransferRequestToTransfer(transferRequest);
                var fromAccount = await _accountsDb.GetById(transfer.Fromaccountid);
                var toAccount = await _accountsDb.GetById(transfer.Toaccountid);

                //switch
                //Validates
                if (fromAccount.UserId != userId) throw new AuthenticationException("User don't owner account");
                if (fromAccount is null || toAccount is null) throw new ArgumentException("Accounts not valid");
                if (fromAccount.Balance < transfer.Amount) throw new ArgumentException("Insufficient funds from your account");
                if (fromAccount.Currency != toAccount.Currency) throw new ArgumentException("Currency isn't the same");

                await _transfersDb.Create(transfer);
                //throw new ArgumentException("Transferencia terminada valida");

                var amount = transfer.Amount;

                //Debit update account
                toAccount.Balance += amount;
                await _accountsDb.Update(toAccount);


                //Credit update account
                amount = amount * -1;
                fromAccount.Balance += amount;
                await _accountsDb.Update(fromAccount);

                await _communicationsBusiness.TransferCommunication(transfer);

                transactionScope.Complete();

                return "Transfer completed";
            }


        }
    }
}
