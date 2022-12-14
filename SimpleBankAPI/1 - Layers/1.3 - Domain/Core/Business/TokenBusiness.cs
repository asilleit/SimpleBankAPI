using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Business
{
    public class TokenBusiness : ITokenBusiness
    {
        protected ITokenRepository _tokenDb;
        protected IUsersRepository _usersDb;
        protected IJwtAuth _jwtAuth;
        protected IConfiguration _config;

        public TokenBusiness(ITokenRepository tokenDb, IUsersRepository usersDb, IJwtAuth jwtAuth, IConfiguration config)
        {
            _tokenDb = tokenDb;
            _usersDb = usersDb;
            _jwtAuth = jwtAuth;
            _config = config;
        }

        public async Task<(User, string, string, DateTime, DateTime)> Revalidate(int userId, RevalidateRequest revalidateRequest)
        {
            //Validate User Login
            User user = await _usersDb.GetById(userId);

            var token = await _tokenDb.GetTokensByRefreshToken(revalidateRequest.RefreshToken);
            if (token == null || user == null) throw new ArgumentException("User not the same!");

            if (!(user.Id.Equals(token.UserId))) throw new ArgumentException("Token not valid!");

            JwtSecurityToken accessToken = _jwtAuth.CreateJwtToken(user);

            var access = new JwtSecurityTokenHandler().WriteToken(accessToken);

            string refreshToken = _jwtAuth.CreateUserRefreshToken();

            token.UserId = user.Id;
            token.RefreshToken = refreshToken;
            token.RefreshTokenExpireAt = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMin"]));

            //Persist Token
            await _tokenDb.Update(token);

            return (user, access, refreshToken, accessToken.ValidTo, token.RefreshTokenExpireAt);
        }

    }
}
