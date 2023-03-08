using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.Todo;
using Todolendar.API.Models.DTO.Auth;

namespace Todolendar.API.Models.Profiles
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
