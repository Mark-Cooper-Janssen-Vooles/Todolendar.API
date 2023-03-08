using Todolendar.API.Models.Domain;

namespace Todolendar.API.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetTodosAsync(Guid userId);
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<Todo> UpateTodoAsync(Guid todoId, Todo todo);
        Task<Todo> DeleteTodoAsync(Guid todoId);
    }
}
