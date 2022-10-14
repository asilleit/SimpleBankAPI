using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models;
using SimpleBankAPI.Data;
using SimpleBankAPI.Models.Response;
using System.Transactions;

namespace SimpleBankAPI.Business
{
    public class AccountsBusiness : IAccountsBusiness 
    {
        protected IAccountsDb _accountsDb;
        public AccountsBusiness(IAccountsDb accountsDb)
        {
            _accountsDb = accountsDb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AccountResponse> Create(AccountRequest accountRequest, int userId)
        {
                try
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

                //UserResponse
                var createAccountResponse = AccountResponse.ToAcountResponse(CreatedAccont);
                return createAccountResponse;
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.ToString());
                }
            
        }

        public async Task<List<Account>> GetAccountsByUser(int userId)
        {

          //  if (await _accountsDb.GetAccountsByUser(userId) is not null)
          //  {
                return await _accountsDb.GetAccountsByUser(userId);
        //    }
            throw new ArgumentException("Account not found");
        }

        public async Task<Account> GetById(int accountId)
        {
            if (await _accountsDb.GetById(accountId) is not null)
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
