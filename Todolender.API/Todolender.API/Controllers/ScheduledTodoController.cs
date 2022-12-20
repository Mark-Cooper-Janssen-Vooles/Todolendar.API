using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;
using Todolender.API.Repositories;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("ScheduledTodo")]
    public class ScheduledTodoController : Controller
    {
        private readonly IMapper mapper;
        private readonly IScheduledTodoRepository scheduledTodoRepository;

        public ScheduledTodoController(IMapper mapper, IScheduledTodoRepository scheduledTodoRepository)
        {
            this.mapper = mapper;
            this.scheduledTodoRepository = scheduledTodoRepository;
        }

        [HttpPost]
        [Route("{userId:guid}")]
        [ActionName("CreateScheduledTodoAsync")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> CreateScheduledTodoAsync([FromRoute] Guid userId, [FromBody] CreateScheduledTodoRequest createScheduledTodoRequest)
        {
            var scheduledTodo = mapper.Map<ScheduledTodo>(createScheduledTodoRequest);
            scheduledTodo = await scheduledTodoRepository.CreateScheduledTodoAsync(userId, scheduledTodo);
            var scheduledTodoDTO = mapper.Map<ScheduledTodoDTO>(scheduledTodo);

            return new CreatedAtActionResult(nameof(CreateScheduledTodoAsync), "ScheduledTodo", new { id = scheduledTodo.Id }, scheduledTodoDTO);
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> GetScheduledTodosAsync([FromRoute] Guid userId, DateRangeRequest dateRangeRequest)
        {
            var scheduledTodos = await scheduledTodoRepository.GetScheduledTodosAsync(userId, dateRangeRequest);
            return Ok(scheduledTodos);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> UpdateScheduledTodoAsync([FromRoute] Guid userId, UpdateScheduledTodoRequest updateScheduledTodoRequest)
        {

        }
    }
}
