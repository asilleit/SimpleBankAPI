using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleBankAPI.JWT
{
    public interface IJwtAuth
    {
        JwtSecurityToken CreateJwtToken(User user);
       // string CreateUserRefreshToken();
        string GetClaim(string authToken, string claimName);

    }
}
