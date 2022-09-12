
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SimpleBankAPI.Models.Response
{
    public class AccountResponse
    {
        public decimal Balance { get; set; }
        [DefaultValue("String")]
        public DateTime CreatedAt { get; set; }
        [Required, MinLength(3), DefaultValue("EUR")]
        public string Currency { get; set; }
        public int Id { get; set; }


       public static AccountResponse ToAcountResponse(Account account)
        {
            var accountResponse = new AccountResponse
            {
                Id = account.Id,
                Balance = account.Balance,
                Currency = account.Currency,
                CreatedAt = account.CreatedAt
            };
            return accountResponse;

    }
    public static List<AccountResponse> FromListAccountsUser(List<Account> accounts)
        {
            var accountResponseList = new List<AccountResponse>();
            foreach (var account in accounts)
            {
                var accountResponse = AccountResponse.ToAcountResponse(account);
                accountResponseList.Add(accountResponse);
            }
            return accountResponseList;
        }
      
    }
}
