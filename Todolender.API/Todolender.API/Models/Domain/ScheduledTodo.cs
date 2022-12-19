namespace Todolender.API.Models.Domain
{
    public class ScheduledTodo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
        public int RecurCount { get; set; }
        public string RecurFrequencyType { get; set; } // another tuple? or create a type 
        public DateTime RecurEndDate { get; set; }
        public int NotifyBeforeTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}
        public DateTime ScheduledAt { get; set; }
        public DateTime TriggeredAt { get; set; }
    }
}
