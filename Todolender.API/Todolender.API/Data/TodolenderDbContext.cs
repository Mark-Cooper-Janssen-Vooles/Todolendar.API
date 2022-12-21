using Microsoft.EntityFrameworkCore;
using Todolender.API.Models.Domain;

namespace Todolender.API.Data
{
    public class TodolenderDbContext : DbContext
    {
        public TodolenderDbContext(DbContextOptions<TodolenderDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PlanReminder> PlanReminder { get; set; }
        public DbSet<ScheduledTodo> ScheduledTodo { get; set; }
        public DbSet<ScheduledTodoChild> ScheduledTodoChild { get; set;}
        public DbSet<Todo> Todos { get; set; }
    }
}
