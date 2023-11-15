using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class TodoTaskDatabaseService : ITodoTaskService
    {
        private readonly TodoListDbContext context;

        public TodoTaskDatabaseService(TodoListDbContext context)
        {
            this.context = context;
        }

        public TodoTask CreateTodoTask(TodoTask todoTask)
        {
            var result = this.context.TodoTasks.Add(new TodoTaskEntity()
            {
                Title = todoTask.Title,
                Description = todoTask.Description,
                Status = Enums.TodoTaskStatus.NotStarted,
                AssignedUserId = todoTask.CreatorUserId,
                CreatorUserId = todoTask.CreatorUserId,
                CreateDate = DateTime.Now,
                DueDate = todoTask.DueDate,
                TodoListId = todoTask.TodoListId,
            });

            _ = this.context.SaveChanges();

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
                TodoListId = result.Entity.TodoListId,
            };
        }

        public void DeleteTodoTask(int todoTaskId)
        {
            var todoTask = this.context.TodoTasks.FirstOrDefault(x => x.Id == todoTaskId);

            if (todoTask != null)
            {
                _ = this.context.TodoTasks.Remove(todoTask);
                _ = this.context.SaveChanges();
            }
        }

        public IQueryable<TodoTask> GetAllTodoTasks()
        {
            return this.context.TodoTasks.Select(x => new TodoTask()
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
            var todoTask = this.context.TodoTasks.FirstOrDefault(x => x.Id == todoTaskId) ?? throw new ArgumentNullException(nameof(todoTaskId), "TodoTask not found");
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
                TodoListId = todoTask.TodoListId,
            };
        }

        public List<TodoTask> GetTodoTasksByTodoList(int todoListId)
        {
            var result = this.context.TodoTasks.Where(x => x.TodoListId == todoListId).ToList();

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
                TodoListId = x.TodoListId,
            }).ToList();
        }

        public TodoTask UpdateTodoTask(int id, TodoTask todoTaskEntity)
        {
            var todoTask = this.context.TodoTasks.Include(x => x.Tags).FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException(nameof(id), "TodoTask not found");

            todoTask.Title = todoTaskEntity.Title;
            todoTask.Description = todoTaskEntity.Description;
            todoTask.Status = todoTaskEntity.Status;
            todoTask.DueDate = todoTaskEntity.DueDate;
            todoTask.AssignedUserId = todoTaskEntity.AssignedUserId;

            if (todoTaskEntity.Tags != null)
            {
                todoTask.Tags = todoTaskEntity.Tags.Select(tag =>
                {
                    var tagEntity = this.context.Tags.FirstOrDefault(x => x.Name == tag.Name);
                    tagEntity ??= new TagEntity()
                    {
                        Name = tag.Name,
                    };

                    return tagEntity;
                }).ToList();
            }

            _ = this.context.SaveChanges();

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
                TodoListId = todoTask.TodoListId,
                Tags = todoTask.Tags?.Select(x => new Tag() { Id = x.Id, Name = x.Name }),
            };
        }
    }
}
