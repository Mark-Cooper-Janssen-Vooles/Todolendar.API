namespace Todolender.API.Models.DTO
{
    public class CreateScheduledTodoRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
        public int RecurCount { get; set; }
        public string RecurFrequencyType { get; set; }
        public DateTime RecurEndDate { get; set; }
        public int NotifyBeforeTime { get; set; }
    }
}