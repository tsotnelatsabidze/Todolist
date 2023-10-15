using Microsoft.EntityFrameworkCore;
using TodoListApp.Services;
using TodoListApp.Services.Database;

public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext context;

    public TodoListDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public async Task<List<TodoList>> GetAll()
    {
        var todoListEntities = await context.TodoLists.ToListAsync();
        var todoLists = todoListEntities.Select(t => new TodoList
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            NumberOfTasks = t.NumberOfTasks,
            IsShared = t.IsShared,
            IsComplete = t.IsComplete,
        }).ToList();
        return todoLists;
    }

    public async Task<TodoList> Get(int id)
    {
        return await context.TodoLists.FindAsync(id);
    }

    public async Task<TodoList> Add(TodoList todoList)
    {
        context.TodoLists.Add((TodoListApp.Services.Database.Entities.TodoListEntity)todoList);
        _ = await this.context.SaveChangesAsync();
        return todoList;
    }

    public async Task Update(int id, TodoList inputTodo)
    {
        var todo = await context.TodoLists.FindAsync(id);
        if (todo != null)
        {
            todo.Title = inputTodo.Title;
            todo.IsComplete = inputTodo.IsComplete;
            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var todo = await context.TodoLists.FindAsync(id);
        if (todo != null)
        {
            context.TodoLists.Remove(todo);
            await context.SaveChangesAsync();
        }
    }
}
