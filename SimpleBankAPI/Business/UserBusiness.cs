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
        protected IJwtAuth _jwtAuth;

        public UserBusiness(IUsersDb usersDb, IJwtAuth jwtAuth)
        {
            _userDb = usersDb;
            _jwtAuth = jwtAuth; 
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest userRequest)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    if (await _userDb.GetByUsername(userRequest.UserName) is not null)
                    {
                        throw new ArgumentException("Username cannot be repeated");
                    }

                    //UserRequest 
                    User user = CreateUserRequest.RequestToUser(userRequest);

                    //Persist User
                    var CreatedUser = await _userDb.Create(user);

                    //Convert user to UserResponse
                    var createUserResponse = CreateUserResponse.ToCreateUserResponse(CreatedUser);
                    transactionScope.Complete();
                    return createUserResponse;
                }      
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();
                    throw new ArgumentException(ex.ToString());
                }
          
            }
        }
        public async Task<User> Login(LoginUserRequest userRequest)
        {
            //Validate User Login
            User user = await _userDb.GetByUsername(userRequest.Username);
            
            if ( user is null) { throw new AuthenticationException("User not found"); }

            if(user.Password != userRequest.Password){ throw new ("Error Password"); }
            return user;
        }

    }
}
