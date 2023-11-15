using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;

namespace TodoListApp.Services.Database
{
    public class TodoListDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }

        public DbSet<TodoListEntity> TodoLists { get; set; }

        public DbSet<TodoTaskEntity> TodoTasks { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }
    }
}
