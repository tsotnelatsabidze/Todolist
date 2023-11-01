namespace TodoListApp.Services.Database.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string CreatorId { get; set; }

        public DateTime CreatDate { get; set; }

        public int TodoTaskId { get; set; }

        public virtual TodoTaskEntity? TodoTask { get; set; }
    }
}
