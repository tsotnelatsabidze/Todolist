namespace TodoListApp.WebApi.Models.Models
{
    public class TodoListViewDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public IEnumerable<TodoTaskDto>? TodoTasks { get; set; }
    }
}
