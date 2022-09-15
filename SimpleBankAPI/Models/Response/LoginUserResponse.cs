using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Models.Response
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public CreateUserResponse User { get; set; }

        public static LoginUserResponse FromUserToLoginUserResponse(User user)
        {
            var userResponse = new LoginUserResponse
            {
                User = CreateUserResponse.ToCreateUserResponse(user),
            };
            return userResponse;
        }
    }
}
