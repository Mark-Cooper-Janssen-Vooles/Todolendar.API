namespace Todolender.API.Models.Domain
{
    public class TodoList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
