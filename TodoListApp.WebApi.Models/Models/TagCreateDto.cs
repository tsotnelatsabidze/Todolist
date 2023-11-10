namespace TodoListApp.WebApi.Models.Models
{
    public class TagCreateDTO
    {
        public string? Name { get; set; }

        public int[]? ToDoTasks { get; set; }
    }
}
