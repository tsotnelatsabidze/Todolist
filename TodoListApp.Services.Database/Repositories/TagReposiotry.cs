using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TagReposiotry : GenericRepository<TagEntity>, ITagReposiotry
    {
        public TagReposiotry(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
