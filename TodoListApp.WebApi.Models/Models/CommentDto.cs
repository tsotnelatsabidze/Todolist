namespace TodoListApp.WebApi.Models.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string? CreatorId { get; set; }

        public DateTime CreatDate { get; set; }

        public int TodoTaskId { get; set; }
    }
}
