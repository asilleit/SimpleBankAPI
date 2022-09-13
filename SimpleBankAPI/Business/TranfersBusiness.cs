using SimpleBankAPI.Data;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
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

            //Validate Account belongs to user
            //if (fromAccount.UserId != userId) throw new AuthenticationException("Account owner and user dont match");
            //Validate Accounts have some currency
            if (fromAccount.Currency != toAccount.Currency) throw new ArgumentException("Currency of destination account is different from origin account");
            //Validate Account have enough funds
            if (fromAccount.Balance < transfer.Amount) throw new ArgumentException("Insufficient funds to make transfer");

            await _transfersDb.Create(transfer);

            var amount = transfer.Amount;
            //
            toAccount.Balance += amount;
            await _accountsDb.Update(toAccount);

            amount = amount * -1;

            toAccount.Balance += amount;
            await _accountsDb.Update(fromAccount);


            return "transfer completed";
        }
    }
}
