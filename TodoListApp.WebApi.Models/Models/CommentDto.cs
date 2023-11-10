namespace TodoListApp.WebApi.Models.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public int TodoTaskId { get; set; }
    }
}
