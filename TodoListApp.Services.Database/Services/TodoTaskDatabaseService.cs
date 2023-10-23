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
    public class TodoTaskDatabaseService : ITodoTaskService
    {
        private readonly TodoListDbContext _context;

        public TodoTaskDatabaseService(TodoListDbContext context)
        {
            _context = context;
        }

        public TodoTask CreateTodoTask(TodoTask todoTask)
        {
            var result = _context.TodoTasks.Add(new TodoTaskEntity()
            {
                Title = todoTask.Title,
                Description = todoTask.Description,
                Status = Enums.TodoTaskStatus.NotStarted,
                AssignedUserId = todoTask.CreatorUserId,
                CreatorUserId = todoTask.CreatorUserId,
                CreateDate = DateTime.Now,
                DueDate = todoTask.DueDate,
                TodoListId = todoTask.TodoListId
            });

            _context.SaveChanges();

            return new TodoTask()
            {
                Id = result.Entity.Id,
                Title = result.Entity.Title,
                Description = result.Entity.Description,
                Status = result.Entity.Status,
                AssignedUserId = result.Entity.AssignedUserId,
                CreatorUserId = result.Entity.CreatorUserId,
                CreateDate = result.Entity.CreateDate,
                DueDate = result.Entity.DueDate,
                TodoListId = result.Entity.TodoListId
            };
        }

        public void DeleteTodoTask(int todoTaskId)
        {
            var todoTask = _context.TodoTasks.FirstOrDefault(x => x.Id == todoTaskId);

            if (todoTask != null)
            {
                _context.TodoTasks.Remove(todoTask);
                _context.SaveChanges();
            }
        }

        public IQueryable<TodoTask> GetAllTodoTasks()
        {
            return _context.TodoTasks.Select(x => new TodoTask()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AssignedUserId = x.AssignedUserId,
                CreatorUserId = x.CreatorUserId,
                CreateDate = x.CreateDate,
                DueDate = x.DueDate,
                TodoListId = x.TodoListId,
                Status = x.Status,
            });
        }

        public TodoTask GetTodoTask(int todoTaskId)
        {
            var todoTask = _context.TodoTasks.FirstOrDefault(x => x.Id == todoTaskId);
            return new TodoTask()
            {
                Id = todoTask.Id,
                Title = todoTask.Title,
                Description = todoTask.Description,
                Status = todoTask.Status,
                AssignedUserId = todoTask.AssignedUserId,
                CreatorUserId = todoTask.CreatorUserId,
                CreateDate = todoTask.CreateDate,
                DueDate = todoTask.DueDate,
                TodoListId = todoTask.TodoListId
            };
        }

        public List<TodoTask> GetTodoTasksByTodoList(int todoListId)
        {
            var result = _context.TodoTasks.Where(x => x.TodoListId == todoListId).ToList();

            return result.Select(x => new TodoTask()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                AssignedUserId = x.AssignedUserId,
                CreatorUserId = x.CreatorUserId,
                CreateDate = x.CreateDate,
                DueDate = x.DueDate,
                TodoListId = x.TodoListId
            }).ToList();
        }

        public TodoTask UpdateTodoTask(int id, TodoTask todoTask)
        {
            var todoTaskEntity = _context.TodoTasks.FirstOrDefault(x => x.Id == id);
            if (todoTaskEntity == null)
            {
                throw new ArgumentNullException("TodoTask not found");
            }

            todoTaskEntity.Title = todoTask.Title;
            todoTaskEntity.Description = todoTask.Description;
            todoTaskEntity.Status = todoTask.Status;
            todoTaskEntity.DueDate = todoTask.DueDate;
            todoTaskEntity.AssignedUserId = todoTask.AssignedUserId;

            _context.SaveChanges();

            return new TodoTask()
            {
                Id = todoTaskEntity.Id,
                Title = todoTaskEntity.Title,
                Description = todoTaskEntity.Description,
                Status = todoTaskEntity.Status,
                AssignedUserId = todoTaskEntity.AssignedUserId,
                CreatorUserId = todoTaskEntity.CreatorUserId,
                CreateDate = todoTaskEntity.CreateDate,
                DueDate = todoTaskEntity.DueDate,
                TodoListId = todoTaskEntity.TodoListId,
            };
        }
    }
}
