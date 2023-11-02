using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentReposiotry
    {
        public CommentRepository(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
