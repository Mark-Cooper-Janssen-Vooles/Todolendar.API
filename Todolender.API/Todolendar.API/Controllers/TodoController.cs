using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.Todo;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Controllers
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
        public async Task<IActionResult> GetTodosAsync([FromRoute] Guid userId)
        {
            var todos = await todoRepository.GetTodosAsync(userId);
            var todosDTO = mapper.Map<IEnumerable<TodoDTO>>(todos);

            return Ok(todosDTO);
        }

        [HttpPost]
        [Route("{userId:guid}")]
        [ActionName("CreateTodoAsync")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> CreateTodoAsync([FromBody] CreateTodoRequest createTodoRequest)
        {
            var todo = mapper.Map<Todo>(createTodoRequest); 
            todo = await todoRepository.CreateTodoAsync(todo);
            var todoDTO = mapper.Map<TodoDTO>(todo);

            return new CreatedAtActionResult(nameof(CreateTodoAsync), "Todo", new { id = todo.Id }, todoDTO);
        }

        [HttpPut]
        [Route("{userId:guid}/{todoId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> UpdateTodoAsync([FromRoute] Guid todoId, [FromBody] UpdateTodoRequest updateTodoRequest)
        {
            var todo = mapper.Map<Todo>(updateTodoRequest);
            todo = await todoRepository.UpateTodoAsync(todoId, todo);
            if (todo == null) return NotFound();
            var todoDTO = mapper.Map<TodoDTO>(todo);

            return Ok(todoDTO);
        }

        [HttpDelete]
        [Route("{userId:guid}/{todoId:guid}")]
        [Authorize(Policy = "user")]
        public async Task<IActionResult> DeleteTodoAsync([FromRoute] Guid todoId)
        {
            var todo = await todoRepository.DeleteTodoAsync(todoId);
            if (todo == null) return NotFound();
            var todoDTO = mapper.Map<TodoDTO>(todo);

            return Ok(todoDTO);
        }
    }
}
