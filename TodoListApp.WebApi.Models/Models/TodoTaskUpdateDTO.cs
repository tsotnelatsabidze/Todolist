using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp.WebApi.Models.Models
{
    public class TodoTaskUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public string AssignedUserId { get; set; }
        public int TodoListId { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
