using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class CommentReposiotry : GenericRepository<CommentEntity>, ICommentReposiotry
    {
        public CommentReposiotry(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
