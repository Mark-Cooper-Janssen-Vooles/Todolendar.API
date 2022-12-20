using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories.Interfaces
{
    public interface IPlanReminderRepository
    {
        Task<PlanReminder> GetPlanReminderAsync(Guid userId);
    }
}
