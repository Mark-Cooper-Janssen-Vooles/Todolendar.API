using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.ScheduledTodo;

namespace Todolender.API.Repositories.Interfaces
{
    public interface IScheduledTodoRepository
    {
        Task<ScheduledTodo> UpdateScheduledTodoAsync(ScheduledTodo scheduledTodo);
        Task<ScheduledTodo> DeleteScheduledTodoAsync(Guid scheduledTodoId);
        Task<ScheduledTodo> CreateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo);
        Task<IEnumerable<ScheduledTodo>> GetScheduledTodosAsync(Guid userId, DateRangeRequest dateRangeRequest);
    }
}
