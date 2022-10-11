using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleBankAPI.JWT
{
    public interface IJwtAuth
    {
        string CreateUserRefreshToken();
        JwtSecurityToken CreateJwtToken(User user);
        string GetClaim(string authToken, string claimName);
        bool TokenIsValid(string authToken);
    }
}
