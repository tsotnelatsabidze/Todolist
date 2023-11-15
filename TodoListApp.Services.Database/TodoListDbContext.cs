using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;

namespace TodoListApp.Services.Database
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoListEntity> TodoLists { get; set; } = null!;

        public DbSet<TodoTaskEntity> TodoTasks { get; set; } = null!;

        public DbSet<TagEntity> Tags { get; set; } = null!;

        public DbSet<CommentEntity> Comments { get; set; } = null!;
    }
}
