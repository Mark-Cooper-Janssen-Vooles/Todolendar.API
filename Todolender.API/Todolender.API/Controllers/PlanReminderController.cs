using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.DTO;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("PlanReminder")]
    public class PlanReminderController : ControllerBase
    {
        private readonly IMapper mapper;

        public PlanReminderController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> GetPlanReminderAsync([FromRoute] Guid userId)
        {
            // talk to repository 
            var planReminder = await planReminderRepository.GetAsync(userId); // if it doesn't exist, create one for them! 
            // convert to DTO
            var planReminderDTO = mapper.Map<PlanReminderDTO>(planReminder);

            return Ok(planReminderDTO);
        }
    }
}
