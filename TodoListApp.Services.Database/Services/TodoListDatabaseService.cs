using AutoMapper;
using AutoMapper.QueryableExtensions;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class TodoListDatabaseService : ITodoListService
    {
        private readonly TodoListDbContext dbContext;

        public TodoListDatabaseService(TodoListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TodoList CreateTodoList(TodoList todoList)
        {
            var result = this.dbContext.TodoLists.Add(new TodoListEntity()
            {
                Name = todoList.Name,
                Description = todoList.Description,
                CreatorUserId = todoList.CreatorUserId,
            });

            _ = this.dbContext.SaveChanges();

            return new TodoList()
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name,
                CreatorUserId = result.Entity.CreatorUserId,
                Description = result.Entity.Description,
            };
        }

        public void DeleteTodoList(int id)
        {
            var todoList = this.dbContext.TodoLists.FirstOrDefault(x => x.Id == id);
            if (todoList == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            _ = this.dbContext.TodoLists.Remove(todoList);
            _ = this.dbContext.SaveChanges();
        }

        public IQueryable<TodoList> GetTodoLists()
        {
            // Create the mapping configuration
            var configuration = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<TodoTaskEntity, TodoTask>().PreserveReferences();
                _ = cfg.CreateMap<TodoListEntity, TodoList>()
                    .ForCtorParam("todoTasks", opt => opt.MapFrom(src => src.TodoTasks)).PreserveReferences();
            });

            // Create the mapper
            IMapper mapper = configuration.CreateMapper();

            // Project the TodoListEntities to TodoList using AutoMapper
            var todoLists = this.dbContext.TodoLists.ProjectTo<TodoList>(mapper.ConfigurationProvider);

            return todoLists;
        }

        public TodoList GetTodoListById(int todoListId)
        {
            var todoListEntity = this.dbContext.TodoLists.FirstOrDefault(x => x.Id == todoListId);
            if (todoListEntity == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            return new TodoList()
            {
                Id = todoListEntity.Id,
                Name = todoListEntity.Name,
                Description = todoListEntity.Description,
            };
        }

        public TodoList UpdateTodoList(int id, TodoList todoList)
        {
            var todoListEntity = this.dbContext.TodoLists.FirstOrDefault(x => x.Id == id);
            if (todoListEntity == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            todoListEntity.Name = todoList.Name;
            todoListEntity.Description = todoList.Description;

            _ = this.dbContext.SaveChanges();

            return new TodoList()
            {
                Id = todoListEntity.Id,
                Name = todoListEntity.Name,
                Description = todoListEntity.Description,
            };
        }
    }
}
