namespace TodoListApp.Services.Database.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public string? Text { get; set; }

        public string? Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public int TodoTaskId { get; set; }

        public virtual TodoTaskEntity? TodoTask { get; set; }
    }
}
