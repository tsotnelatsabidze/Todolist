using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TodoTaskReposiotry : GenericRepository<TodoTaskEntity>, ITodoTaskRepository
    {
        public TodoTaskReposiotry(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
