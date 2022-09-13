using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IUserBusiness
    {
        Task<CreateUserResponse> Create(CreateUserRequest userRequest);
        Task<User> Login(LoginUserRequest userRequest);
    }
}
