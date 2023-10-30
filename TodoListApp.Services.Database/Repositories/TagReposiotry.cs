using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;

namespace TodoListApp.Services.Database.Repositories
{
    public class TagReposiotry : GenericRepository<TagEntity>, ITagReposiotry
    {
        public TagReposiotry(TodoListDbContext dbContext) : base(dbContext)
        {
        }
    }
}
