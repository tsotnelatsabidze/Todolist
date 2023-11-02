using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Database.Repositories;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class CommentService : ICommentService
    {
        public CommentService(ICommentReposiotry commentReposiotry)
        {
            this.CommentReposiotry = commentReposiotry;
        }

        public ICommentReposiotry CommentReposiotry { get; set; }

        public Comment AddComment(string comment)
        {
            var newComment = new Entities.CommentEntity()
            {
                Comment = comment,
            };

            this.CommentReposiotry.Insert(newComment);

            return new Comment()
            {
                Id = newComment.Id,
                Name = newComment.Comment,
            };
        }

        public void DeleteComment(int id)
        {
            var comment = CommentReposiotry.GetById(id);

            if (comment == null)
            {
                throw new ArgumentNullException("Comment not found");
            }

            CommentReposiotry.Delete(comment);

        }

        public Comment GetComment(int id)
        {
            var commentEntity = this.CommentReposiotry.GetById(id);
            return new Comment()
            {
                Id = commentEntity.Id,
                Name = commentEntity.Comment,
            };
        }
    }
}
