namespace Todolendar.API.Models.Domain
{
    public enum RecurFrequency
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
    }

    public class ScheduledTodo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
        public int RecurCount { get; set; }
        public RecurFrequency RecurFrequencyType { get; set; } // another tuple? or create a type 
        public DateTime RecurEndDate { get; set; }
        public int NotifyBeforeTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}
        public DateTime ScheduledAt { get; set; }
        public DateTime? TriggeredAt { get; set; }
    }
}
