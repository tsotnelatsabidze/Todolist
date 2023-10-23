using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}
