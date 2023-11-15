using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TagRepository : GenericRepository<TagEntity>, ITagRepository
    {
        public TagRepository(TodoListDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
