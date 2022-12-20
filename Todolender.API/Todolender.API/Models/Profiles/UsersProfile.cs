using AutoMapper;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Models.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, CreateUserRequest>().ReverseMap();

            CreateMap<User, UpdateUserRequest>().ReverseMap();
        }
    }
}
