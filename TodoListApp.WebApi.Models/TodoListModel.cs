namespace TodoListApp.WebApi.Models
{
    public class TodoListModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int NumberOfTasks { get; set; }

        public bool IsShared { get; set; }
    }
}
