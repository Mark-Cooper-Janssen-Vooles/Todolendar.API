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
    }
}
