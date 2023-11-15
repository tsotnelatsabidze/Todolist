namespace TodoListApp.Services.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Text { get; set; }

        public string? Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public int TodoTaskId { get; set; }
    }
}
