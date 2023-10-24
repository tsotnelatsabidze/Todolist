namespace TodoListApp.WebApi.Models.Models
{
    public class TodoTaskCreateDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public int TodoListId { get; set; }

        public int CreatorUserId { get; set; }
    }
}
