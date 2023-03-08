namespace Todolendar.API.Models.DTO.ScheduledTodo
{
    public class UpdateScheduledTodoRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
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