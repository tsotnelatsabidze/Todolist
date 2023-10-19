namespace TodoListApp.Services
{
    public interface ITodoTaskService
    {
        ICollection<TodoTask> GetTodoTasks();

        TodoTask GetTodoTask(int id);

        TodoTask GetTodoTask(string title);

        TodoTask GetTodoTaskTrimToUpper(TodoTask todoTaskCreate);

        bool TodoTaskExists(int taskId);

        bool CreateTodoTask(int ownerId, int todoListId, TodoTask todoTask);

        bool UpdateTodoTask(int ownerId, int todoListId, TodoTask todoTask);

        bool DeleteTodoTask(TodoTask todoTask);

        bool Save();
    }
}
