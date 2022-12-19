namespace Todolender.API.Models.Domain
{
    public class PlanReminder
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool PlanReminderOn { get; set; }
        public string? Frequency { get; set; } // this could be a tuple
        public DateTime? NextScheduledAt { get; set; }
        public string? Description { get; set; }

    }
}
