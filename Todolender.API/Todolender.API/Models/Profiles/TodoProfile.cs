using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.Auth;
using Todolender.API.Models.DTO.Todo;

namespace Todolender.API.Models.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, CreateTodoRequest>().ReverseMap();
            CreateMap<Todo, TodoDTO>().ReverseMap();
            CreateMap<Todo, UpdateTodoRequest>().ReverseMap();
        }
    }
}
