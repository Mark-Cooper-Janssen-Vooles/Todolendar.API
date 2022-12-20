using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories
{
    public interface IScheduledTodoRepository
    {
        Task<ScheduledTodo> CreateScheduledTodoAsync(Guid userId, ScheduledTodo scheduledTodo);
    }
}
