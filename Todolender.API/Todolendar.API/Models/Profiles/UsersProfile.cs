using AutoMapper;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.Auth;

namespace Todolendar.API.Models.Profiles
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
