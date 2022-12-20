
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
            CreateMap<CreateAccount, AccountRequest>();
            CreateMap<Transfer, TransferRequest>();
            CreateMap<CreateUserRequest, CreateUser>().ReverseMap();
            CreateMap<AccountDetails, GetAccountResponse>().ReverseMap();
            //CreateMap<Data.Models.Movim, Data.Services.Base.Movim>().ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();
        }
    }
}
