namespace TodoListApp.WebApi.Models.Models
{
    public class TodoTaskUpdateDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public int Status { get; set; }

        public int AssignedUserId { get; set; }
    }
}
