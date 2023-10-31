using AutoMapper;
using AutoMapper.QueryableExtensions;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class TodoListDatabaseService : ITodoListService
    {
        private readonly TodoListDbContext _dbContext;

        public TodoListDatabaseService(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TodoList CreateTodoList(TodoList todoList)
        {
            var result = _dbContext.TodoLists.Add(new TodoListEntity()
            {
                Name = todoList.Name,
                Description = todoList.Description,
                CreatorUserId = todoList.CreatorUserId,
            });

            _dbContext.SaveChanges();

            return new TodoList()
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name,
                CreatorUserId = result.Entity.CreatorUserId,
                Description = result.Entity.Description
            };
        }

        public void DeleteTodoList(int id)
        {
            var todoList = _dbContext.TodoLists.FirstOrDefault(x => x.Id == id);
            if (todoList == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            _dbContext.TodoLists.Remove(todoList);
            _dbContext.SaveChanges();
        }

        public IQueryable<TodoList> GetTodoLists()
        {
            // Create the mapping configuration
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoTaskEntity, TodoTask>().PreserveReferences();
                cfg.CreateMap<TodoListEntity, TodoList>()
                    .ForCtorParam("todoTasks", opt => opt.MapFrom(src => src.TodoTasks)).PreserveReferences();
            });

            // Create the mapper
            IMapper mapper = configuration.CreateMapper();

            // Project the TodoListEntities to TodoList using AutoMapper
            var todoLists = _dbContext.TodoLists.ProjectTo<TodoList>(mapper.ConfigurationProvider);

            return todoLists;
        }




        public TodoList GetTodoListById(int todoListId)
        {
            var todoListEntity = _dbContext.TodoLists.FirstOrDefault(x => x.Id == todoListId);
            if (todoListEntity == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            return new TodoList()
            {
                Id = todoListEntity.Id,
                Name = todoListEntity.Name,
                Description = todoListEntity.Description
            };
        }

        public TodoList UpdateTodoList(int id, TodoList todoList)
        {
            var todoListEntity = _dbContext.TodoLists.FirstOrDefault(x => x.Id == id);
            if (todoListEntity == null)
            {
                throw new ArgumentNullException("TodoList not found");
            }

            todoListEntity.Name = todoList.Name;
            todoListEntity.Description = todoList.Description;

            _dbContext.SaveChanges();

            return new TodoList()
            {
                Id = todoListEntity.Id,
                Name = todoListEntity.Name,
                Description = todoListEntity.Description
            };
        }
    }
}
