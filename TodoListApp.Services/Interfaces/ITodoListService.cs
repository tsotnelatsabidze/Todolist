using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITodoListService
    {
        TodoList CreateTodoList(TodoList todoList);

        void DeleteTodoList(int id);

        public IQueryable<TodoList> GetTodoLists();

        TodoList GetTodoListById(int todoListId);

        TodoList UpdateTodoList(int id, TodoList todoList);
    }
}
