using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.DTO;
using Todolender.API.Repositories.Interfaces;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("PlanReminder")]
    public class PlanReminderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPlanReminderRepository planReminderRepository;

        public PlanReminderController(IMapper mapper, IPlanReminderRepository planReminderRepository)
        {
            this.mapper = mapper;
            this.planReminderRepository = planReminderRepository;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> GetPlanReminderAsync([FromRoute] Guid userId)
        {
            // talk to repository 
            var planReminder = await planReminderRepository.GetPlanReminderAsync(userId); // if it doesn't exist, create one for them! 
            // convert to DTO
            var planReminderDTO = mapper.Map<PlanReminderDTO>(planReminder);

            return Ok(planReminderDTO);
        }
    }
}
