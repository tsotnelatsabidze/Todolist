using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ICommentService
    {
        public Comment AddComment(string comment);

        public Comment GetComment(int id);
    }
}
