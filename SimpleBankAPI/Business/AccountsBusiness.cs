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
                throw new ArgumentException("Amount invalid");
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
            if (_accountsDb.GetAccountsByUser(userId) is not null)
            {
                return await _accountsDb.GetAccountsByUser(userId);
            }
            throw new ArgumentException("Account not found");
        }

        public async Task<Account> GetById(int accountId)
        {
            if(_accountsDb.GetById(accountId) is not null)
            {
                return await _accountsDb.GetById(accountId);
            }
            throw new ArgumentException("Account not found");
        }

        public void Update(Account accountToUpdate)
        {
            _accountsDb.Update(accountToUpdate);
        }

    }
}
