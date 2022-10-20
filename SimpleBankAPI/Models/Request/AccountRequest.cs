using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class AccountRequest
    {
        [Required]
        public int Amount { get; set; }
        [Required, MinLength(3), DefaultValue("EUR")]
        public string Currency { get; set; }

        public static Account FromUserRequestToAccount(AccountRequest accontReq, int userId)
        {
            var account = new Account()
            {
                Balance = accontReq.Amount,
                CreatedAt = DateTime.UtcNow,
                Currency = accontReq.Currency,
                UserId = userId,
            };
            return account;
        }
    }
}
