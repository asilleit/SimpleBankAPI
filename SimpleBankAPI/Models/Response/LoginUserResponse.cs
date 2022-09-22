using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Models.Response
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public CreateUserResponse User { get; set; }

        public static LoginUserResponse FromUserToLoginUserResponse(User user, JwtSecurityToken token)
        {
            var userResponse = new LoginUserResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpiresAt = token.ValidFrom,
                User = CreateUserResponse.ToCreateUserResponse(user),
            };
            return userResponse;
        }
    }
}
