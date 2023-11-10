using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class TagsDatabaseService : ITagService
    {
        public ITagReposiotry TagReposiotry { get; set; }

        public ITodoTaskReposiotry TodoTaskReposiotry { get; set; }

        private IMapper _mapper { get; set; }

        public TagsDatabaseService(ITagReposiotry tagReposiotry, IMapper mapper, ITodoTaskReposiotry todoTaskReposiotry)
        {
            TagReposiotry = tagReposiotry;
            _mapper = mapper;
            TodoTaskReposiotry = todoTaskReposiotry;
        }

        public Tag CreateTag(int todoTaskId, string tag)
        {
            var tagEntity = TagReposiotry.GetAll().Where(x => x.Name == tag).Include(x => x.TodoTasks).FirstOrDefault();
            var todoTaskEntity = TodoTaskReposiotry.GetAll().Where(x => x.Id == todoTaskId).Include(x => x.Tags).FirstOrDefault();

            if (tagEntity == null)
            {
                tagEntity = new Entities.TagEntity()
                {
                    Name = tag,
                    TodoTasks = new List<Entities.TodoTaskEntity>() { todoTaskEntity }
                };
            }
            else if (todoTaskEntity.Tags.Any(x => x.Name == tag))
            {
                return _mapper.Map<Tag>(todoTaskEntity.Tags.FirstOrDefault(x => x.Name == tag));
            }
            else
            {
                tagEntity.TodoTasks.Add(todoTaskEntity);
            }

            TagReposiotry.Insert(tagEntity);

            return _mapper.Map<Tag>(tagEntity);
        }

        public Tag GetTag(int id)
        {
            var tagEntity = TagReposiotry.GetById(id);
            return _mapper.Map<Tag>(tagEntity);
        }

        public void DeleteTag(string name)
        {
            var tagEntity = TagReposiotry.GetAll().FirstOrDefault(x => x.Name == name);
            if (tagEntity == null)
            {
                throw new Exception("Tag not found");
            }
            else
            {
                TagReposiotry.Delete(tagEntity);
            }
        }

        public void DeleteTagFromTodoTask(int todoTaskId, string name)
        {
            var todoTask = TodoTaskReposiotry.GetAll().Where(x => x.Id == todoTaskId).Include(x => x.Tags).FirstOrDefault();
            if (todoTask == null)
            {
                throw new Exception("todoTask not found");
            }
            else
            {
                todoTask.Tags = todoTask.Tags.Where(x => x.Name != name).ToList();
                TodoTaskReposiotry.Update(todoTask);
            }
        }

        public IQueryable<Tag> GetAll()
        {
            return this.TagReposiotry.GetAll().ProjectTo<Tag>(_mapper.ConfigurationProvider);
        }
    }
}
