namespace TodoListApp.Services;

public class TodoTask
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreationDate { get; set; }

    public ICollection<TodoTaskTodoList>? TodoTaskTodoLists { get; set; }
}
