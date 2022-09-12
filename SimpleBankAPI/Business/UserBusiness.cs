using Microsoft.AspNetCore.Identity;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using SimpleBankAPI.Models;

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

            //Persist User
            var CreatedUser = await _userDb.Create(user);

            //Convert user to UserResponse
            var createUserResponse = CreateUserResponse.ToCreateUserResponse(CreatedUser);
            return createUserResponse;
        }
    }
}
