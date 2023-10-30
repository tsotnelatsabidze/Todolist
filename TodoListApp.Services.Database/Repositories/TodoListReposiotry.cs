using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TodoListRepository : GenericRepository<TodoListEntity>, ITodoListRepository
    {
        public TodoListRepository(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
