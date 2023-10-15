namespace TodoListApp.Services.Database.Entities;
public class TodoTask
{
    public int TodoListId { get; set; }

    public TodoList TodoList { get; set; }

    public int Id { get; set; }
}
