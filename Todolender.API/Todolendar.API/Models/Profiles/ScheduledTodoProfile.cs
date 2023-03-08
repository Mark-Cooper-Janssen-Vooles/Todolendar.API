using AutoMapper;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.ScheduledTodo;

namespace Todolendar.API.Models.Profiles
{
    public class ScheduledTodoProfile : Profile
    {
        public ScheduledTodoProfile() 
        {
            CreateMap<ScheduledTodo, ScheduledTodoDTO>().ReverseMap();

            CreateMap<ScheduledTodo, CreateScheduledTodoRequest>().ReverseMap();

            CreateMap<ScheduledTodo, UpdateScheduledTodoRequest>().ReverseMap();
        }
    }
}
