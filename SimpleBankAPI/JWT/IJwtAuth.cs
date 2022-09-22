using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleBankAPI.JWT
{
    public interface IJwtAuth
    {

        public JwtSecurityToken GenerateUserToken(User username);
        public ClaimsPrincipal GetPrincipal(string token);
        //public string GetToken(string token);
    }
}
