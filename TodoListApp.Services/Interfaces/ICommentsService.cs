using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ICommentsService
    {
        public IQueryable<Comment> GetCommentsByTaskId(int taskId);

        public Comment GetCommentById(int commentId);

        public Comment CreateComment(Comment comment);

        public void DeleteComment(int commentId);

        IQueryable<Comment> GetAll();
    }
}
