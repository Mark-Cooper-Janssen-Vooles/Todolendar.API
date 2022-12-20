using AutoMapper;
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
        [Route("{id:guid}")]
        public async Task<IActionResult> CreateScheduledTodoAsync(Guid userId, CreateScheduledTodoRequest createScheduledTodoRequest)
        {
            // validations 

            // map DTO to domain 
            var scheduledTodo = mapper.Map<ScheduledTodo>(createScheduledTodoRequest);

            // use repository to create and return scheduled todo domain model
            scheduledTodo = await scheduledTodoRepository.CreateScheduledTodoAsync(userId, scheduledTodo);

            // map domain model to DTO 

            // return status to user with DTO

            // put auth in 
        }
    }
}
