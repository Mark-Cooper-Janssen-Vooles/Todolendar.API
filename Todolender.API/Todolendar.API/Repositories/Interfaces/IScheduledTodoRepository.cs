using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO.ScheduledTodo;

namespace Todolendar.API.Repositories.Interfaces
{
    public interface IScheduledTodoRepository
    {
        Task<ScheduledTodo> UpdateScheduledTodoAsync(ScheduledTodo scheduledTodo);
        Task<ScheduledTodo> DeleteScheduledTodoAsync(Guid scheduledTodoId);
        Task<ScheduledTodo> CreateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo);
        Task<IEnumerable<ScheduledTodo>> GetScheduledTodosAsync(Guid userId, DateRangeRequest dateRangeRequest);
    }
}
