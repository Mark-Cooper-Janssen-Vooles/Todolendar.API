using System;
using Todolender.API.Data;
using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories
{
    public class ScheduledTodoRepository : IScheduledTodoRepository
    {
        private readonly TodolenderDbContext dbContext;

        public ScheduledTodoRepository(TodolenderDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<ScheduledTodo> CreateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo)
        {
            scheduledTodo.Id = Guid.NewGuid();
            scheduledTodo.UserId = userId;
            scheduledTodo.Active = scheduledTodo.ScheduledAt > DateTime.UtcNow; // need to check if this works with js vs c#. there are other options
            scheduledTodo.CreatedAt = DateTime.UtcNow;
            scheduledTodo.LastUpdatedAt = DateTime.UtcNow;

            await dbContext.ScheduledTodo.AddAsync(scheduledTodo);
            await dbContext.SaveChangesAsync();

            return scheduledTodo;
        }
    }
}