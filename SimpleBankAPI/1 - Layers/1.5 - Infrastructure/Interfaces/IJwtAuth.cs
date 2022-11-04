using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Interfaces
{
    public interface IJwtAuth
    {
        string CreateUserRefreshToken();
        JwtSecurityToken CreateJwtToken(User user);
        string GetClaim(string authToken, string claimName);
        bool TokenIsValid(string authToken);
    }
}
