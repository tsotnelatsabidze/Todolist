namespace TodoListApp.Services
{
    public interface ITodoListService
    {
        Task<List<TodoList>> GetAll();

        Task<TodoList> Get(int id);

        Task<TodoList> Add(TodoList todo);

        Task Update(int id, TodoList todo);

        Task Delete(int id);
    }
}
