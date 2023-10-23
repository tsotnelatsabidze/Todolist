using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Description = todoList.Description
            });

            _dbContext.SaveChanges();

            return new TodoList()
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name,
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

        public List<TodoList> GetTodoLists()
        {
            return _dbContext.TodoLists.Select(x => new TodoList
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
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
