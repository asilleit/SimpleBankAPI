using Microsoft.IdentityModel.Tokens;
using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SimpleBankAPI.JWT
{
    public class JwtAuth : IJwtAuth 
    {
        private static string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private readonly IConfiguration _config;

        public JwtAuth(IConfiguration configuration)
        {
            _config = configuration;
        }
        //public JwtSecurityToken GenerateUserToken(User username)
        //{
        //    byte[] key = Convert.FromBase64String(Secret);
        //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        //    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //             new Claim(ClaimTypes.Name, username.Username)}),
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SigningCredentials = new SigningCredentials(securityKey,
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //    JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        //    return token;
        //}
        //public ClaimsPrincipal GetPrincipal(string token)
        //{
        //    try
        //    {
        //        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //        JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
        //        if (jwtToken == null)
        //            return null;
        //        byte[] key = Convert.FromBase64String(Secret);
        //        TokenValidationParameters parameters = new TokenValidationParameters()
        //        {
        //            RequireExpirationTime = true,
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            IssuerSigningKey = new SymmetricSecurityKey(key)
        //        };
        //        SecurityToken securityToken;
        //        ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
        //              parameters, out securityToken);
        //        return principal;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        ////public static string ValidateToken(string token)
        ////{
        ////    string username = null;
        ////    ClaimsPrincipal principal = GetPrincipal(token);
        ////    if (principal == null)
        ////        return null;
        ////    ClaimsIdentity identity = null;
        ////    try
        ////    {
        ////        identity = (ClaimsIdentity)principal.Identity;
        ////    }
        ////    catch (NullReferenceException)
        ////    {
        ////        return null;
        ////    }
        ////    Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
        ////    username = usernameClaim.Value;
        ////    return username;
        ////}


        public JwtSecurityToken CreateJwtToken(User user)
        {
            
            //Claims
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim("user", user.Id.ToString())
                };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            // Create Token
            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);
            return token;
        }

        
        public string GetClaim(string authToken, string claimName)
        {
            authToken = authToken.Replace("Bearer ", ""); 
            JwtSecurityToken token = new JwtSecurityToken(authToken);

            return token.Claims.FirstOrDefault(claim => claim.Type == claimName).Value;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]))
            };
            return tokenValidationParameters;
        }


    }
}
