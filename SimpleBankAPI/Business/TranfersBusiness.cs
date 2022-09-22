using SimpleBankAPI.Data;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using System.Security.Authentication;
using System.Transactions;

namespace SimpleBankAPI.Business
{
    public class TransfersBusiness : ITransfersBusiness
    {
        protected ITransfersDb _transfersDb;
        protected IAccountsDb _accountsDb;

        public TransfersBusiness(ITransfersDb transfersDb,  IAccountsDb accountsDb)
        {
            _transfersDb = transfersDb;
            _accountsDb = accountsDb;
        }
        public async Task<string> Create(TransferRequest transferRequest, int userId)
        {

                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Convert TransferRequest to Transfer
                    var transfer = TransferRequest.FromTransferRequestToTransfer(transferRequest);
                    var fromAccount = await _accountsDb.GetById(transfer.Fromaccountid);
                    var toAccount = await _accountsDb.GetById(transfer.Toaccountid);


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
                    //colocar exceção para validar os inserts!! para apanhar o transation
                    await _accountsDb.Update(toAccount);

                    //Credit update account
                    amount = amount * -1;
                    toAccount.Balance += amount;
                    await _accountsDb.Update(fromAccount);

                    transactionScope.Complete();
                    return "Transfer completed";
                }
                
        }
    }
}
