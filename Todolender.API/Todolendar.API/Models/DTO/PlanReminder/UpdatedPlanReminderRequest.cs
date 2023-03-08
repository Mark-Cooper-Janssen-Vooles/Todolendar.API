namespace Todolendar.API.Models.DTO.PlanReminder
{
    public class UpdatedPlanReminderRequest
    {
        public bool PlanReminderOn { get; set; }
        public string? Frequency { get; set; }
        public DateTime? NextScheduledAt { get; set; }
        public string? Description { get; set; }
    }
}
