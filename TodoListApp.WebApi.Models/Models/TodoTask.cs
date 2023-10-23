namespace TodoListApp.WebApi.Models.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public int Status { get; set; }

        public int CreatorUserId { get; set; }

        public int AssignedUserId { get; set; }

        public int TodoListId { get; set; }
    }
}
