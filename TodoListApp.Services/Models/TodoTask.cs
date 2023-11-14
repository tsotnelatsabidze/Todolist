using TodoListApp.Services.Enums;

namespace TodoListApp.Services.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public TodoTaskStatus Status { get; set; }

        public string? CreatorUserId { get; set; }

        public string AssignedUserId { get; set; }

        public int TodoListId { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
