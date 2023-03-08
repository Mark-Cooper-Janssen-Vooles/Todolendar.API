using AutoMapper;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.PlanReminder;

namespace Todolendar.API.Models.Profiles
{
    public class PlanReminderProfile : Profile
    {
        public PlanReminderProfile()
        {
            CreateMap<PlanReminder, PlanReminderDTO>().ReverseMap();

            CreateMap<PlanReminder, UpdatedPlanReminderRequest>().ReverseMap();
        }
    }
}