using AutoMapper;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.ScheduledTodo;

namespace Todolender.API.Models.Profiles
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
