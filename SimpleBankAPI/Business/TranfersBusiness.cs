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
        public async Task<string> Create(TransferRequest transferRequest)
        {
            //Convert TransferRequest to Transfer
            var transfer = TransferRequest.FromTransferRequestToTransfer(transferRequest);
            var fromAccount = await _accountsDb.GetById(transfer.Fromaccountid);
            var toAccount = await _accountsDb.GetById(transfer.Toaccountid);

            //Validate Balance
            if (fromAccount.Balance < transfer.Amount) throw new ArgumentException("Insufficient funds");
            if(fromAccount is null || toAccount is null) throw new ArgumentException("Accounts not valid");

            await _transfersDb.Create(transfer);

            var amount = transfer.Amount;
            
            //Debit update account
            toAccount.Balance += amount;
            await _accountsDb.Update(toAccount);

            //Credit update account
            amount = amount * -1;
            toAccount.Balance += amount;
            await _accountsDb.Update(fromAccount);

            return "Transfer completed";
        }

    }
}
