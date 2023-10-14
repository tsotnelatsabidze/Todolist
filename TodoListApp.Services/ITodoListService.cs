namespace TodoListApp.Services
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync();

        Task<TodoList> GetTodoListByTitleAsync(string title);

        Task<TodoList> AddTodoListAsync(TodoList todoList);

        Task UpdateTodoListAsync(TodoList todoList);

        Task DeleteTodoListAsync(string title);
    }
}
