namespace TodoListApp.WebApi.Models.Models
{
    public class TagCreateDto
    {
        public string? Name { get; set; }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
        public int[]? ToDoTasks { get; set; }
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
    }
}
