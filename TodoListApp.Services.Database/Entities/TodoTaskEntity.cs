using TodoListApp.Services.Enums;

namespace TodoListApp.Services.Database.Entities
{
    public class TodoTaskEntity
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public TodoTaskStatus Status { get; set; }

        public int CreatorUserId { get; set; }

        public int AssignedUserId { get; set; }

        public virtual TodoListEntity? TodoList { get; set; }

        public int TodoListId { get; set; }
    }
}
