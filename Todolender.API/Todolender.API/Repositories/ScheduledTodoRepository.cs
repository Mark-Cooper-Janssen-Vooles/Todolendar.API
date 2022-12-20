using Microsoft.EntityFrameworkCore;
using System;
using Todolender.API.Data;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

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
            scheduledTodo.TriggeredAt = null;

            // TODO: add logic for scheduledTodoChild 
            if (scheduledTodo.RecurCount > 0)
            {
                // take scheduled at time (this todo) 
                // look at recurFrequency time (i.e. weekly or daily)
                // create ScheduledTodoChildren based on 

                // create scheduled todo children => need to iterate through to make them all. 
            }

            await dbContext.ScheduledTodo.AddAsync(scheduledTodo);
            await dbContext.SaveChangesAsync();

            return scheduledTodo;
        }

        public async Task<IEnumerable<ScheduledTodo>> GetScheduledTodosAsync(Guid userId, DateRangeRequest dateRangeRequest)
        {
            var scheduledTodos = await dbContext.ScheduledTodo
                .Where(x => 
                (x.UserId == userId) && // needs correct userId
                (x.ScheduledAt >= dateRangeRequest.StartDate && x.ScheduledAt <= dateRangeRequest.EndDate)) // only between a certain date range
                .ToListAsync();

            return scheduledTodos;
        }

        public async Task<ScheduledTodo> UpdateScheduledTodoAsync(ScheduledTodo updatedScheduledTodo)
        {
            var existingScheduledTodo = await dbContext.ScheduledTodo.FirstOrDefaultAsync(x => x.Id == updatedScheduledTodo.Id);
            if (existingScheduledTodo == null) return null;

            existingScheduledTodo.Title = updatedScheduledTodo.Title;
            existingScheduledTodo.Description = updatedScheduledTodo.Description;
            existingScheduledTodo.Colour= updatedScheduledTodo.Colour;
            existingScheduledTodo.RecurCount= updatedScheduledTodo.RecurCount;
            existingScheduledTodo.RecurFrequencyType= updatedScheduledTodo.RecurFrequencyType;
            existingScheduledTodo.RecurEndDate= updatedScheduledTodo.RecurEndDate;
            existingScheduledTodo.NotifyBeforeTime= updatedScheduledTodo.NotifyBeforeTime;
            existingScheduledTodo.ScheduledAt= updatedScheduledTodo.ScheduledAt;
            existingScheduledTodo.LastUpdatedAt = DateTime.UtcNow;


            // TODO: if recurCount went from 0 to more, need to put in logic to create ScheduledTodoChildren 
            if (updatedScheduledTodo.RecurCount > 0)
            {
                // take scheduled at time (this todo) 
                // look at recurFrequency time (i.e. weekly or daily)
                // create ScheduledTodoChildren based on 

                // create scheduled todo children => need to iterate through to make them all. 
            }

            // TODO: and vice-versa, delete scheduledTodoChildren if recurCount reduced in number. 

            await dbContext.SaveChangesAsync();
            return existingScheduledTodo;
        }
    }
}