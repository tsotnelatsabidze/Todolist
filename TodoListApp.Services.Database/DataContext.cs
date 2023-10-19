using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;

namespace TodoListApp.Services.Database
{
    public class DataContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }

        public DbSet<TodoListEntity> TodoLists => this.Set<TodoListEntity>();

        public DbSet<TodoTask> TodoTasks { get; set; }

        public DbSet<TodoTaskTodoList> TodoTaskTodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<TodoTaskTodoList>()
                    .HasKey(ttl => new { ttl.TodoTaskId, ttl.TodoListId });
            _ = modelBuilder.Entity<TodoTaskTodoList>()
                    .HasOne(t => t.TodoTask)
                    .WithMany(ttl => ttl.TodoTaskTodoLists)
                    .HasForeignKey(t => t.TodoTaskId);
            _ = modelBuilder.Entity<TodoTaskTodoList>()
                    .HasOne(t => t.TodoList)
                    .WithMany(ttl => ttl.TodoTaskTodoLists)
                    .HasForeignKey(t => t.TodoListId);
        }
    }
}
