using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;

namespace TodoListApp.Services.Database
{
    public class TodoListDbContext : DbContext
    {
        public DbSet<TodoListEntity> TodoLists { get; set; }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }
    }
}
