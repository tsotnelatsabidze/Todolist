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

        public DbSet<TodoListEntity> TodoLists { get; set; }

        public DbSet<TodoTaskEntity> TodoTasks { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }
    }
}
