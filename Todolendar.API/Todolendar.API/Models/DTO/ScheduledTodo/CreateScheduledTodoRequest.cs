namespace Todolendar.API.Models.DTO.ScheduledTodo
{
    public class CreateScheduledTodoRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Colour { get; set; }
        public int RecurCount { get; set; }
        public string RecurFrequencyType { get; set; }
        public DateTime RecurEndDate { get; set; }
        public int NotifyBeforeTime { get; set; }
        public DateTime ScheduledAt { get; set; }
    }
}

// recurFrequencyType can be 'daily', 'weekly', 'monthly'