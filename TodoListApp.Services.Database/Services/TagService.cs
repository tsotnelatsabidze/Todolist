using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class TagService : ITagService
    {
        public ITagReposiotry TagReposiotry { get; set; }

        public TagService(ITagReposiotry tagReposiotry)
        {
            this.TagReposiotry = tagReposiotry;
        }

        public Tag CreateTag(string tag)
        {
            var newTag = new Entities.TagEntity()
            {
                Name = tag,
            };

            this.TagReposiotry.Insert(newTag);

            return new Tag()
            {
                Id = newTag.Id,
                Name = newTag.Name,
            };
        }

        public Tag GetTag(int id)
        {
            var tagEntity = this.TagReposiotry.GetById(id);
            return new Tag()
            {
                Id = tagEntity.Id,
                Name = tagEntity.Name,
            };
        }
    }
}
