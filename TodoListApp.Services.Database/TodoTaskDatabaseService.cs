using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database
{
    public class TodoTaskDatabaseService : ITodoTaskService
    {
        private readonly DataContext _context;

        public TodoTaskDatabaseService(DataContext context)
        {
            _context = context;
        }

        public bool CreateTodoTask(TodoTask todoTask)
        {
            _context.Add(todoTask);
            return Save();
        }

        public bool DeleteTodoTask(TodoTask todoTask)
        {
            _context.Remove(todoTask);
            return Save();
        }

        public TodoTask GetTodoTask(int id)
        {
            return _context.TodoTasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public TodoTask GetTodoTask(string title)
        {
            return _context.TodoTasks.Where(t => t.Title == title).FirstOrDefault();
        }


        public ICollection<TodoTask> GetTodoTasks()
        {
            return _context.TodoTasks.ToList();
        }
        public TodoTask GetTodoTaskTrimToUpper(TodoTask todoTaskCreate)
        {
            return GetTodoTasks().FirstOrDefault(c => c.Title.Trim().ToUpper() == todoTaskCreate.Title.TrimEnd().ToUpper());
        }

        public bool TodoTaskExists(int taskId)
        {
            return _context.TodoTasks.Any(t => t.Id == taskId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTodoTask(int ownerId, int todoListId, TodoTask todoTask)
        {
            _context.Update(todoTask);
            return Save();
        }
    }
}
