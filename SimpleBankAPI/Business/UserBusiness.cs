using Microsoft.AspNetCore.Identity;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using SimpleBankAPI.Models;
using System.Security.Authentication;

namespace SimpleBankAPI.Business
{
    public class UserBusiness : IUserBusiness
    {
        protected IUsersDb _userDb;
        public UserBusiness(IUsersDb usersDb)
        {
            _userDb = usersDb;
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest userRequest)
        {
           
            //UserRequest 
            User user = CreateUserRequest.RequestToUser(userRequest);

            if (_userDb.GetByUsername(userRequest.UserName) is not null)
            {
                throw new ArgumentException("Username cannot be repeated");
            }
            if (_userDb.GetByUsername(userRequest.UserName) is not null)
            {
                throw new ArgumentException("Username cannot be repeated");
            }

            //Persist User
            var CreatedUser = await _userDb.Create(user);

            //Convert user to UserResponse
            var createUserResponse = CreateUserResponse.ToCreateUserResponse(CreatedUser);
            return createUserResponse;
        }
        public async Task<User> Login(LoginUserRequest userRequest)
        {
            //Validate User Login
            User user = await _userDb.GetByUsername(userRequest.Username);
            
            if ( user is null) { throw new AuthenticationException("User not found"); }

            if(user.Password != userRequest.Password)
            {
                throw new AuthenticationException("Error Password");
            }
            return user;
        }
    }
}
