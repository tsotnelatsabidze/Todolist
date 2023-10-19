namespace TodoListApp.Services
{
    public interface ITodoListService
    {
        ICollection<TodoList> GetTodoLists();

        TodoList GetTodoList(int id);

        ICollection<TodoTask> GetTodoTasksByTodoList(int todoListId);

        bool TodoListExists(int id);

        bool CreateTodoList(TodoList todoList);

        bool UpdateTodoList(TodoList todoList);

        bool DeleteTodoList(TodoList todoList);

        bool Save();

    }
}
