using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TodoTaskReposiotry : GenericRepository<TodoTaskEntity>, ITodoTaskReposiotry
    {
        public TodoTaskReposiotry(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
