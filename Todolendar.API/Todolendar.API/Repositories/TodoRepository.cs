using Microsoft.EntityFrameworkCore;
using Todolendar.API.Data;
using Todolendar.API.Models.Domain;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodolendarDbContext dbContext;

        public TodoRepository(TodolendarDbContext dbContext)
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

        public async Task<Todo> DeleteTodoAsync(Guid todoId)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
            if (todo == null) return null;

            dbContext.Todos.Remove(todo);
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
