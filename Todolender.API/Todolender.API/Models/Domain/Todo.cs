namespace Todolender.API.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public Guid TodoListId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
