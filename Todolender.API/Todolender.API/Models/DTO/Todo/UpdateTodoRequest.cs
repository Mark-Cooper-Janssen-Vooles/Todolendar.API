namespace Todolender.API.Models.DTO.Todo
{
    public class UpdateTodoRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
