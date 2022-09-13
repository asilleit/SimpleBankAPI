using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models;
using SimpleBankAPI.Data;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Business
{
    public class AccountsBusiness : IAccountsBusiness
    {
        protected IAccountsDb _accountsDb;
        public AccountsBusiness(IAccountsDb accountsDb)
        {
            _accountsDb = accountsDb;
        }

        public async Task<AccountResponse> Create(AccountRequest accountRequest, int userId)
        {
            //Validate arguments
            //AccountRequest 
            if (accountRequest.Amount < 0)
            {
                throw new ArgumentException("amount or currency not valid");
            }

            Account accont = AccountRequest.FromUserRequestToAccount(accountRequest, userId);

            //Persist account
            var CreatedAccont = await _accountsDb.Create(accont);

            //Convert account to UserResponse

            var createAccountResponse = AccountResponse.ToAcountResponse(CreatedAccont);
            return createAccountResponse;
        }

        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            return await _accountsDb.GetAccountsByUser(userId);
        }

        public async Task<Account> GetById(int accountId)
        {
            return await _accountsDb.GetById(accountId);

        }

        public void Update(Account accountToUpdate)
        {
            _accountsDb.Update(accountToUpdate);
        }

    }
}
