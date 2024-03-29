﻿using Microsoft.EntityFrameworkCore;
using System;
using Todolendar.API.Data;
using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.ScheduledTodo;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
{
    public class ScheduledTodoRepository : IScheduledTodoRepository
    {
        private readonly TodolendarDbContext dbContext;

        public ScheduledTodoRepository(TodolendarDbContext dbContext) 
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
                var childrenToCreate = scheduledTodo.RecurCount;
                // create scheduledTodo Children
                for (int i = childrenToCreate; i > 1; i--)
                {
                    DateTime newScheduledAtTime = scheduledTodo.ScheduledAt;

                    // 3+1, 3 = 1
                    // 3+1, 2 = 2
                    // 3+1, 1 = 3
                    var currentIteration = scheduledTodo.RecurCount + 1 - i;

                    if ((int)scheduledTodo.RecurFrequencyType == 1) // daily
                    {
                        newScheduledAtTime = newScheduledAtTime.AddDays(currentIteration);
                    }

                    if ((int)scheduledTodo.RecurFrequencyType == 2) // weekly
                    {
                        newScheduledAtTime = newScheduledAtTime.AddDays(7 * currentIteration);
                    }

                    if ((int)scheduledTodo.RecurFrequencyType == 3) // monthly
                    {
                        newScheduledAtTime = newScheduledAtTime.AddMonths(currentIteration);
                    }

                    if ((int)scheduledTodo.RecurFrequencyType == 4) // yearly
                    {
                        newScheduledAtTime = newScheduledAtTime.AddYears(currentIteration);
                    }

                    var childScheduledTodo = new ScheduledTodo()
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Title = scheduledTodo.Title,
                        Description = scheduledTodo.Description,
                        Colour = scheduledTodo.Colour,
                        Active = scheduledTodo.Active,
                        RecurCount = 0,
                        RecurFrequencyType = 0,
                        RecurEndDate = scheduledTodo.RecurEndDate, // this isn't a proper value. don't think we need this
                        NotifyBeforeTime = scheduledTodo.NotifyBeforeTime,
                        CreatedAt = scheduledTodo.CreatedAt,
                        LastUpdatedAt = scheduledTodo.LastUpdatedAt,
                        ScheduledAt = newScheduledAtTime,
                        TriggeredAt = scheduledTodo.TriggeredAt,
                    };

                    await dbContext.ScheduledTodo.AddAsync(childScheduledTodo);
                    await dbContext.SaveChangesAsync();
                }
            }

            await dbContext.ScheduledTodo.AddAsync(scheduledTodo);
            await dbContext.SaveChangesAsync();

            return scheduledTodo;
        }

        public async Task<ScheduledTodo> DeleteScheduledTodoAsync(Guid scheduledTodoId)
        {
            var scheduledTodo = await dbContext.ScheduledTodo.FirstOrDefaultAsync(x => x.Id == scheduledTodoId);
            if (scheduledTodo == null) return null;

            dbContext.ScheduledTodo.Remove(scheduledTodo);
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