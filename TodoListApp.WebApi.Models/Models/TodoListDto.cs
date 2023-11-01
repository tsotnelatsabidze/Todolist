namespace TodoListApp.WebApi.Models.Models
{
    public class TodoListDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public IEnumerable<TodoTaskDto>? TodoTasks { get; set; }
    }
}
