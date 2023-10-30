using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Interfaces;

namespace TodoListApp.Services.Database.Interfaces
{
    public interface ITodoTaskReposiotry : IRepository<TodoTaskEntity>
    {
    }
}
