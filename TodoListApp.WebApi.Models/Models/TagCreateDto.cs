namespace TodoListApp.WebApi.Models.Models
{
    public class TagCreateDto
    {
        public string? Name { get; set; }

        public int[]? ToDoTasks { get; set; }
    }
}
