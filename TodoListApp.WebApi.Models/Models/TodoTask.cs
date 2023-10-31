namespace TodoListApp.WebApi.Models.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public int Status { get; set; }

        public string CreatorUserId { get; set; }

        public string AssignedUserId { get; set; }

        public int TodoListId { get; set; }

        public IList<TagDto> Tags { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "Not Started";
                    case 1:
                        return "In Progress";
                    case 2:
                        return "Completed";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
