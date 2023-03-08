namespace Todolendar.API.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
