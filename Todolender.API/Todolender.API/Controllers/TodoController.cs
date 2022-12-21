using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.Todo;
using Todolender.API.Repositories;
using Todolender.API.Repositories.Interfaces;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("Todo")]
    public class TodoController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITodoRepository todoRepository;

        public TodoController(IMapper mapper, ITodoRepository todoRepository)
        {
            this.mapper = mapper;
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> GetAllTodosAsync([FromRoute] Guid userId)
        {
            var todos = await todoRepository.GetAllTodosAsync(userId);

            return Ok(todos);
        }

        [HttpPost]
        [Route("{userId:guid}")]
        [ActionName("CreateTodoAsync")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> CreateTodoAsync([FromRoute] Guid userId, [FromBody] CreateTodoRequest createTodoRequest)
        {
            var todo = mapper.Map<Todo>(createTodoRequest); 
            todo = await todoRepository.CreateTodoAsync(todo);
            var todoDTO = mapper.Map<TodoDTO>(todo);

            return new CreatedAtActionResult(nameof(CreateTodoAsync), "Todo", new { id = todo.Id }, todoDTO);
        }
    }
}
