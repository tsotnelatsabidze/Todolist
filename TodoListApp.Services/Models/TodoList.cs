namespace TodoListApp.Services.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? CreatorUserId { get; set; }

        public IEnumerable<TodoTask>? TodoTasks { get; set; }
    }
}
