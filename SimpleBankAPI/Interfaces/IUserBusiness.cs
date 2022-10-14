using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleBankAPI.Interfaces
{
    public interface IUserBusiness
    {
        Task<CreateUserResponse> Create(CreateUserRequest userRequest);
        Task<(User, string, string, DateTime, DateTime)> Login(LoginUserRequest userRequest);

    }
}
