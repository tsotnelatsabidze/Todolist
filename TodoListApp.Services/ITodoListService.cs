namespace TodoListApp.Services
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync();

        Task<TodoList> GetTodoListByTitleAsync(string title);

        Task<TodoList> AddTodoList(TodoList todoList);

        Task UpdateTodoListAsync(TodoList todoList);

        Task DeleteTodoListAsync(string title);
    }
}
