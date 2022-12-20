using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("ScheduledTodo")]
    public class ScheduledTodoController
    {
        private readonly IMapper mapper;

        public ScheduledTodoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheduledTodoAsync(CreateScheduledTodoRequest createScheduledTodoRequest)
        {
            // validations 

            // map DTO to domain 
            var scheduledTodo = mapper.Map<ScheduledTodo>(createScheduledTodoRequest);

            // use repository to create and return scheduled todo domain model

            // map domain model to DTO 

            // return status to user with DTO

            // put auth in 
        }
    }
}
