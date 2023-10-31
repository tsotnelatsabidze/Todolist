namespace TodoListApp.WebApi.Models.Models
{
    public class TodoListCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorUserId { get; set; }
    }
}
