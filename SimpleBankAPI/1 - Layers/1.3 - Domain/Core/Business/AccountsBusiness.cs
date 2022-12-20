using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Business
{
    public class AccountsBusiness : IAccountsBusiness
    {
        protected IAccountsRepository _accountsDb;
        protected IConfiguration _config;
        public AccountsBusiness(IAccountsRepository accountsDb)
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
                if (accountRequest.Amount < 0) throw new ArgumentException("Amount invalid");

                var accont = AccountRequest.FromUserRequestToAccount(accountRequest, userId);

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
            try
            {
                return await _accountsDb.GetAccountsByUser(userId);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }

        public async Task<Account> GetById(int id)
        {
            var account = await _accountsDb.GetById(id);
            return account;
        }

        public async void Update(Account accountToUpdate)
        {
            _accountsDb.Update(accountToUpdate);
        }
    }
}
