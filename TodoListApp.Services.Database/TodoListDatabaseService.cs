using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database
{
    public class TodoListDatabaseService : ITodoListService
    {
        private readonly TodoListDbContext context;

        public TodoListDatabaseService(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsAsync()
        {
            return await this.context.TodoLists.ToListAsync();
        }

        public async Task<TodoList> GetTodoListByTitleAsync(string title)
        {
            return await this.context.TodoLists.FindAsync(title);
        }

        public async Task<TodoList> AddTodoListAsync(TodoList todoList)
        {
            _ = this.context.TodoLists.Add((Entities.TodoListEntity)todoList);
            _ = await this.context.SaveChangesAsync();
            return todoList;
        }

        public async Task UpdateTodoListAsync(TodoList todoList)
        {
            this.context.Entry(todoList).State = EntityState.Modified;
            _ = await this.context.SaveChangesAsync();
        }

        public async Task DeleteTodoListAsync(string title)
        {
            var todoList = await this.context.TodoLists.FindAsync(title);
            if (todoList != null)
            {
                _ = this.context.TodoLists.Remove(todoList);
                _ = await this.context.SaveChangesAsync();
            }
        }
    }
}
