namespace TodoListApp.Services.Database.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<TodoTaskEntity>? TodoTasks { get; set; }
    }
}
