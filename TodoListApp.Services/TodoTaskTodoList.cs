namespace TodoListApp.Services;
public class TodoTaskTodoList
{
    public int TodoTaskId { get; set; }

    public int TodoListId { get; set; }

    public TodoTask? TodoTask { get; set; }

    public TodoList? TodoList { get; set; }
}
