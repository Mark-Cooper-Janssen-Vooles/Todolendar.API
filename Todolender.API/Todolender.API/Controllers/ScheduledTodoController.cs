using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.DTO;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("ScheduledTodo")]
    public class ScheduledTodoController
    {
        [HttpPost]
        public async Task<IActionResult> CreateScheduledTodoAsync(CreateScheduledTodoRequest createScheduledTodoRequest)
        {
            // validations 

            // map DTO to domain 

            // use repository to create and return scheduled todo domain model

            // map domain model to DTO 

            // return status to user with DTO

            // put auth in 
        }
    }
}
