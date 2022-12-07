using AutoMapper;
using Blazor.Data.Models;
using Blazor.Data.Services.Base;

namespace Blazor
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserRequest>().ReverseMap();
        }

    }
}
