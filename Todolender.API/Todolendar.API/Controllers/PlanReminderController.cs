using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.PlanReminder;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Controllers
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
            var planReminder = await planReminderRepository.GetPlanReminderAsync(userId); // if it doesn't exist, create one for them! 
            var planReminderDTO = mapper.Map<PlanReminderDTO>(planReminder);

            return Ok(planReminderDTO);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> UpdatePlanReminderAsync([FromRoute] Guid userId, [FromBody] UpdatedPlanReminderRequest updatePlanReminderRequest)
        {
            var planReminder = mapper.Map<PlanReminder>(updatePlanReminderRequest);
            planReminder = await planReminderRepository.UpdatePlanReminderAsync(userId, planReminder);
            if (planReminder == null) return NotFound();
            var planReminderDTO = mapper.Map<PlanReminderDTO>(planReminder);

            return Ok(planReminderDTO);
        }
    }
}
