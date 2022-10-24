using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Business
{
    public class TokenBusiness : ITokenBusiness
    {
        private ITokenDb _tokenDb;
        private IUsersDb _usersDb;
        private IJwtAuth _jwtAuth;
        private IConfiguration _config;

        public TokenBusiness(ITokenDb tokenDb, IUsersDb usersDb, IJwtAuth jwtAuth, IConfiguration config)
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

            Token token = await _tokenDb.GetTokensByRefreshToken(revalidateRequest.RefreshToken);
            if (token == null || user == null)
            {
                throw new ArgumentException();
            }

            if (!(user.Id.Equals(token.UserId)))
            {
                throw new ArgumentException();
            }


            JwtSecurityToken accessToken = _jwtAuth.CreateJwtToken(user);

            var access = new JwtSecurityTokenHandler().WriteToken(accessToken);

            string refreshToken = _jwtAuth.CreateUserRefreshToken();

            token.UserId = user.Id;
            token.Refresh_token = refreshToken;
            token.Refresh_token_expire_at = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMin"]));

            //token.User = user;


            //Persist Token
            await _tokenDb.Update(token);



            return (user, access, refreshToken, accessToken.ValidTo, token.Refresh_token_expire_at);
        }

    }
}
