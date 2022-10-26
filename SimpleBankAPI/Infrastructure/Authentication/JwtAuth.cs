using Microsoft.IdentityModel.Tokens;
using SimpleBankAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleBankAPI.Interfaces.Provider
{
    public class JwtAuth : IJwtAuth
    {
        // private static string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private readonly IConfiguration _config;

        public JwtAuth(IConfiguration configuration)
        {
            _config = configuration;
        }

        public JwtSecurityToken CreateJwtToken(User user)
        {
            //Claims
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim("user", user.Id.ToString()),
                };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            // Create Token
            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: credentials);

            //var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);
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

        public string CreateUserRefreshToken()
        {

            //Claims
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            // Create Token
            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMin"])),
                signingCredentials: credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenToReturn;
        }

        public bool TokenIsValid(string authToken)
        {
            TokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = GetTokenValidationParameters();
            Task<TokenValidationResult> tokenValidationResult = tokenHandler.ValidateTokenAsync(authToken, validationParameters);
            if (tokenValidationResult.Result.IsValid)
            {
                return true;
            }
            return false;
        }
    }
}
