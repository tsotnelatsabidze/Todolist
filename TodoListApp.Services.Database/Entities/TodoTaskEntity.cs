using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Entities;
public class TodoTaskEntity : TodoTask
{
    public int TodoListId { get; set; }

    public TodoList? TodoList { get; set; }

    public new int Id { get; set; }
}
