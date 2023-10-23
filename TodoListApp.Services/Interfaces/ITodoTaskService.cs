using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITodoTaskService
    {
        TodoTask CreateTodoTask(TodoTask todoTask);

        void DeleteTodoTask(int todoTaskId);

        IQueryable<TodoTask> GetAllTodoTasks();

        TodoTask GetTodoTask(int todoTaskId);

        List<TodoTask> GetTodoTasksByTodoList(int todoListId);

        TodoTask UpdateTodoTask(int id, TodoTask todoTaskEntity);
    }
}
