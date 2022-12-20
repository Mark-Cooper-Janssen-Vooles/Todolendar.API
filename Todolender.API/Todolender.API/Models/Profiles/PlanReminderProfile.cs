using AutoMapper;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Models.Profiles
{
    public class PlanReminderProfile : Profile
    {
        public PlanReminderProfile()
        {
            CreateMap<PlanReminder, PlanReminderDTO>().ReverseMap();
        }
    }
}