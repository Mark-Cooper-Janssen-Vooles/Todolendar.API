using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.ScheduledTodo;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Controllers
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

        [HttpPost] // essentially a GET but it has a body...?! Info out there is conflicting if GET's can have bodys, AXIOS doesn't want to play with it nicely.
        [Route("get/{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> GetScheduledTodosAsync([FromRoute] Guid userId, DateRangeRequest dateRangeRequest) // note: shouldn't send body to GET requests
        {
            var scheduledTodos = await scheduledTodoRepository.GetScheduledTodosAsync(userId, dateRangeRequest);

            return Ok(scheduledTodos);
        }

        [HttpPut]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> UpdateScheduledTodoAsync(UpdateScheduledTodoRequest updateScheduledTodoRequest)
        {
            var scheduledTodo = mapper.Map<ScheduledTodo>(updateScheduledTodoRequest);
            scheduledTodo = await scheduledTodoRepository.UpdateScheduledTodoAsync(scheduledTodo);
            var scheduledTodoDTO = mapper.Map<ScheduledTodoDTO>(scheduledTodo);

            return Ok(scheduledTodoDTO);
        }

        [HttpDelete]
        [Route("{userId:guid}/{scheduledTodoId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> DeleteScheduledTodoAsync([FromRoute] Guid scheduledTodoId)
        {
            var scheduledTodo = await scheduledTodoRepository.DeleteScheduledTodoAsync(scheduledTodoId);
            if (scheduledTodo == null) return NotFound();

            var scheduledTodoDTO = mapper.Map<ScheduledTodoDTO>(scheduledTodo);
            return Ok(scheduledTodoDTO);
        }
    }
}
