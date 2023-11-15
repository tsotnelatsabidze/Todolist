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
        public TagsDatabaseService(ITagRepository tagRepository, IMapper mapper, ITodoTaskRepository todoTaskRepository)
        {
            this.TagRepository = tagRepository;
            this.Mapper = mapper;
            this.TodoTaskReposiotry = todoTaskRepository;
        }

        public ITagRepository TagRepository { get; set; }

        public ITodoTaskRepository TodoTaskReposiotry { get; set; }

        private IMapper Mapper { get; set; }

        public Tag CreateTag(int todoTaskId, string tag)
        {
            var tagEntity = this.TagRepository.GetAll().Where(x => x.Name == tag).Include(x => x.TodoTasks).FirstOrDefault();
            var todoTaskEntity = this.TodoTaskReposiotry.GetAll().Where(x => x.Id == todoTaskId).Include(x => x.Tags).FirstOrDefault();

            if (tagEntity == null)
            {
                tagEntity = new Entities.TagEntity()
                {
                    Name = tag,
                    TodoTasks = new List<Entities.TodoTaskEntity>() { todoTaskEntity },
                };
            }
            else if (todoTaskEntity.Tags != null && todoTaskEntity.Tags.Any(x => x.Name == tag))
            {
                return this.Mapper.Map<Tag>(todoTaskEntity.Tags.FirstOrDefault(x => x.Name == tag));
            }
            else
            {
                tagEntity.TodoTasks.Add(todoTaskEntity);
            }

            this.TagRepository.Insert(tagEntity);

            return this.Mapper.Map<Tag>(tagEntity);
        }

        public Tag GetTag(int id)
        {
            var tagEntity = this.TagRepository.GetById(id);
            return this.Mapper.Map<Tag>(tagEntity);
        }

        public void DeleteTag(string name)
        {
            var tagEntity = this.TagRepository.GetAll().FirstOrDefault(x => x.Name == name);
            if (tagEntity == null)
            {
                throw new InvalidOperationException("Tag not found");
            }
            else
            {
                this.TagRepository.Delete(tagEntity);
            }
        }

        public void DeleteTagFromTodoTask(int todoTaskId, string name)
        {
            var todoTask = this.TodoTaskReposiotry.GetAll().Where(x => x.Id == todoTaskId).Include(x => x.Tags).FirstOrDefault();
            if (todoTask == null)
            {
                throw new ArgumentException("TodoTask not found");
            }
            else
            {
                if (todoTask.Tags != null)
                {
                    todoTask.Tags = todoTask.Tags.Where(x => x.Name != name).ToList();
                }

                this.TodoTaskReposiotry.Update(todoTask);
            }
        }

        public IQueryable<Tag> GetAll()
        {
            return this.TagRepository.GetAll().ProjectTo<Tag>(this.Mapper.ConfigurationProvider);
        }
    }
}
