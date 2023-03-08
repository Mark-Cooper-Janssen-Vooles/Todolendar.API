using Microsoft.EntityFrameworkCore;
using Todolendar.API.Models.Domain;

namespace Todolendar.API.Data
{
    public class TodolendarDbContext : DbContext
    {
        public TodolendarDbContext(DbContextOptions<TodolendarDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PlanReminder> PlanReminder { get; set; }
        public DbSet<ScheduledTodo> ScheduledTodo { get; set; }
        public DbSet<ScheduledTodoChild> ScheduledTodoChild { get; set;}
        public DbSet<Todo> Todos { get; set; }
    }
}
