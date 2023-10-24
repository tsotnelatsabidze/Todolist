namespace TodoListApp.Services.Database.Entities
{
    public class TodoListEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int CreatorUserId { get; set; }

        public virtual ICollection<TodoTaskEntity>? TodoTasks { get; set; }
    }
}
