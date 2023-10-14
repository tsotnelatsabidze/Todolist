using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services;
public class TodoList
{
    [Key]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public int NumberOfTasks { get; set; }

    public bool IsShared { get; set; }
}
