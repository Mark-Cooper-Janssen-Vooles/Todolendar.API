namespace Todolendar.API.Models.DTO.Todo
{
    public class CreateTodoRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
