namespace SimpleBankAPI.Models.Response
{
    public class GetAccountResponse
    {
        public AccountResponse account { get; set; }
        public List<Movim> movims { get; set; }
        

        public static GetAccountResponse FromAccountToAccountResponse(Account account)
        {
            var accountResponse = new GetAccountResponse
            {
                account = AccountResponse.ToAcountResponse(account),
                movims = new List<Movim>()
            };
            foreach (var movement in account.Movements)
            {
                accountResponse.movims.Add(Movim.FromMovementToMovim(movement));
            }
            return accountResponse;
        }
    }

}
