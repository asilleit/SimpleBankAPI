using NuGet.Common;
using SimpleBankAPI.Models.Request;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Models.Response
{
    public class RevalidateResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiresAt { get; set; }


        public static RevalidateResponse CreateRevalidateResponse(Token token, string authorization)
        {
            //JwtSecurityToken authorization1 = new JwtSecurityToken(authorization);


            var createRevalidateResponse = new RevalidateResponse
            {

                AccessToken = authorization,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(5),
                //RefreshToken = authorization,
                //RefreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(10),
            };
            return createRevalidateResponse;
        }
    }
}
