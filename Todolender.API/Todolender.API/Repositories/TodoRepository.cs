using Microsoft.EntityFrameworkCore;
using Todolender.API.Data;
using Todolender.API.Models.Domain;
using Todolender.API.Repositories.Interfaces;

namespace Todolender.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodolenderDbContext dbContext;

        public TodoRepository(TodolenderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Todo> CreateTodoAsync(Todo todo)
        { 
            todo.Id = Guid.NewGuid();
            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(Guid userId)
        {
            var todos = await dbContext.Todos.Where(x => x.UserId == userId).ToListAsync();

            return todos;
        }

        public async Task<Todo> UpateTodoAsync(Guid todoId, Todo todo)
        {
            var existingTodo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
            if (existingTodo == null) return null;

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;

            await dbContext.SaveChangesAsync();

            return existingTodo;
        }
    }
}
