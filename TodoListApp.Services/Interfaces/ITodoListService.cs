using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITodoListService
    {
        TodoList CreateTodoList(TodoList toDoList);

        void DeleteTodoList(int id);

        List<TodoList> GetTodoLists();

        TodoList GetTodoListById(int todoListId);

        TodoList UpdateTodoList(int id, TodoList todoList);
    }
}
