
using System.Reflection;
using AutoMapper;
using Blazor.Data.Models;
using Blazor.Data.Services.Base;

using System.Linq.Expressions;
using AutoMapper.Configuration;

namespace Blazor
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<CreateAccount, AccountRequest>();
            CreateMap<CreateUserRequest, CreateUser>().ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
        }
    }
}
