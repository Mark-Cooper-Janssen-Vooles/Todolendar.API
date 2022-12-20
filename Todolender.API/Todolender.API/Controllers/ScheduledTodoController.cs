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
    public class ScheduledTodoController
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
        [Authorize(Policy = "user")] // check this works?!
        public async Task<IActionResult> CreateScheduledTodoAsync([FromRoute] Guid userId, [FromBody] CreateScheduledTodoRequest createScheduledTodoRequest)
        {
            // map DTO to domain 
            var scheduledTodo = mapper.Map<ScheduledTodo>(createScheduledTodoRequest);

            // use repository to create and return scheduled todo domain model
            scheduledTodo = await scheduledTodoRepository.CreateScheduledTodoAsync(userId, scheduledTodo);

            // map domain model to DTO 
            var scheduledTodoDTO = mapper.Map<ScheduledTodoDTO>(scheduledTodo);

            // return status to user with DTO
            return new CreatedAtActionResult(nameof(CreateScheduledTodoAsync), "ScheduledTodo", new { id = scheduledTodo.Id }, scheduledTodoDTO);

            // put auth in 
        }
    }
}
