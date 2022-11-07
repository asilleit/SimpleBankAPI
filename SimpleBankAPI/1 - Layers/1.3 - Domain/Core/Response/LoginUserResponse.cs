namespace SimpleBankAPI.Models.Response
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpiresAt { get; set; }
        public CreateUserResponse User { get; set; }

        public LoginUserResponse(User user, string acess, string refresh, DateTime dateAcess, DateTime dateRefresh)
        {
            AccessToken = acess;
            AccessTokenExpiresAt = dateAcess;

            RefreshToken = refresh;
            RefreshExpiresAt = dateRefresh;

            User = CreateUserResponse.ToCreateUserResponse(user);

        }

    }
}
