using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITodoListService
    {
        ICollection<TodoList> GetTodoLists();
        TodoList GetTodoList(int id);
        bool TodoListExists(int id);
        bool CreateTodoList(TodoList todoList);
        bool UpdateTodoList(TodoList todoList);
        bool DeleteTodoList(TodoList todoList);
        bool Save();
    }
}
