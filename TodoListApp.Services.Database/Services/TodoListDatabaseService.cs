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

        public TodoList CreateTodoList(TodoList toDoList)
        {
            var result = this.dbContext.TodoLists.Add(new TodoListEntity()
            {
                Name = toDoList.Name,
                Description = toDoList.Description,
            });

            _ = this.dbContext.SaveChanges();

            return new TodoList()
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name,
                Description = result.Entity.Description,
            };
        }

        public void DeleteTodoList(int id)
        {
            var todoList = this.dbContext.TodoLists.FirstOrDefault(x => x.Id == id);
            if (todoList == null)
            {
                throw new ArgumentNullException(nameof(todoList), "TodoList not found");
            }

            _ = this.dbContext.TodoLists.Remove(todoList);
            _ = this.dbContext.SaveChanges();
        }

        public List<TodoList> GetTodoLists()
        {
            return this.dbContext.TodoLists.Select(x => new TodoList
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();
        }

        public TodoList GetTodoListById(int todoListId)
        {
            var todoListEntity = this.dbContext.TodoLists.FirstOrDefault(x => x.Id == todoListId);
            if (todoListEntity == null)
            {
                throw new ArgumentNullException(nameof(todoListEntity), "TodoList not found");
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
                throw new ArgumentNullException(nameof(todoListEntity), "TodoList not found");
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
