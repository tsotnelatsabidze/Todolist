using Microsoft.EntityFrameworkCore;
using TodoListApp.Services;

namespace TodoListApp.Services.Database
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoList> TodoList => Set<TodoList>();
    }
}
