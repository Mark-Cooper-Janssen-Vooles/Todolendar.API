using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> CreateTodoAsync(Guid userId, Todo todo);
    }
}
