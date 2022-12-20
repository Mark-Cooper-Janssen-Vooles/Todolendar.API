﻿using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Repositories
{
    public interface IScheduledTodoRepository
    {
        Task<ScheduledTodo> UpdateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo);
        Task<ScheduledTodo> CreateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo);
        Task<IEnumerable<ScheduledTodo>> GetScheduledTodosAsync(Guid userId, DateRangeRequest dateRangeRequest);
    }
}
