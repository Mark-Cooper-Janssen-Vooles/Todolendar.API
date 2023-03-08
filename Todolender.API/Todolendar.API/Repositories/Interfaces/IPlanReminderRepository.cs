using Todolendar.API.Models.Domain;

namespace Todolendar.API.Repositories.Interfaces
{
    public interface IPlanReminderRepository
    {
        Task<PlanReminder> GetPlanReminderAsync(Guid userId);
        Task<PlanReminder> UpdatePlanReminderAsync(Guid userId, PlanReminder planReminder);
    }
}
