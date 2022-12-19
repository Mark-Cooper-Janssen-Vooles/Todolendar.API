namespace Todolender.API.Models.Domain
{
    public class ScheduledTodoChild
    {
        public Guid Id { get; set; }
        public Guid ScheduledTodoId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
        public int NotifyBeforeTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime TriggeredAt { get; set; }
    }
}
