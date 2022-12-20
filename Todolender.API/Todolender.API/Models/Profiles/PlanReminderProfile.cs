using AutoMapper;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.PlanReminder;

namespace Todolender.API.Models.Profiles
{
    public class PlanReminderProfile : Profile
    {
        public PlanReminderProfile()
        {
            CreateMap<PlanReminder, PlanReminderDTO>().ReverseMap();

            CreateMap<PlanReminder, UpdatePlanReminderRequest>().ReverseMap();
        }
    }
}