using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database.Entities
{
    [Table("todo_list")]
    public class TodoListEntity : TodoList
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
