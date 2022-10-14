using Microsoft.AspNetCore.Identity;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using SimpleBankAPI.Models;
using System.Security.Authentication;
using System.Transactions;
using SimpleBankAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using SimpleBankAPI.JWT;

namespace SimpleBankAPI.Business
{
    public class UserBusiness : IUserBusiness
    {
        protected IUsersDb _userDb;
        protected ITokenDb _tokenDb;
        protected IJwtAuth _jwtAuth;
        protected IConfiguration _config;

        public UserBusiness(IUsersDb usersDb, IJwtAuth jwtAuth, ITokenDb tokenDb, IConfiguration _configuration)
        {
            _userDb = usersDb;
            _jwtAuth = jwtAuth;
            _tokenDb = tokenDb;
            _config = _configuration;
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest userRequest)
        {
                try
                {
                    //if (await _userDb.GetByUsername(userRequest.UserName) is not null)
                    //{
                    //    throw new ArgumentException("Username cannot be repeated");
                    //}

                    //UserRequest 
                    User user = CreateUserRequest.RequestToUser(userRequest);

                    //Persist User
                    var CreatedUser = await _userDb.Create(user);

                    //Convert user to UserResponse
                    var createUserResponse = CreateUserResponse.ToCreateUserResponse(CreatedUser);
                    return createUserResponse;
                }      
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.ToString());
                }

        }
        public async Task<(User, string, string, DateTime, DateTime)> Login(LoginUserRequest userRequest)
        {
            //Validate User Login
            User user = await _userDb.GetByUsername(userRequest.Username);

            JwtSecurityToken accessToken = _jwtAuth.CreateJwtToken(user);

            var access = new JwtSecurityTokenHandler().WriteToken(accessToken);

            string refreshToken = _jwtAuth.CreateUserRefreshToken();

            Token token = new Token();

            token.UserId = user.Id;
            token.Refresh_token = refreshToken;
            token.Refresh_token_expire_at = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMin"]));

            //token.User = user;

            
            //Persist Token
            await _tokenDb.Create(token);

            if ( user is null) { throw new AuthenticationException("User not found"); }

            if(user.Password != userRequest.Password){ throw new ("Error Password"); }
            return (user, access, refreshToken,accessToken.ValidTo,token.Refresh_token_expire_at) ;
        }

    }
}
