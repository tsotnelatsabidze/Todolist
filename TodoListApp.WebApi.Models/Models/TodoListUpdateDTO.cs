namespace TodoListApp.WebApi.Models.Models
{
    public class TodoListUpdateDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
