using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database
{
    public class TodoListDatabaseService : ITodoListService
    {
        private DataContext _context;

        public TodoListDatabaseService(DataContext context)
        {
            this._context = context;
        }

        public bool TodoListExists(int id)
        {
            return this._context.TodoLists.Any(t => t.Id == id);
        }

        public bool CreateTodoList(TodoList todoList)
        {
            this._context.Add(todoList);
            return Save();
        }

        public bool DeleteTodoList(TodoList todoList)
        {
            _context.Remove(todoList);
            return Save();
        }

        public ICollection<TodoList> GetTodoLists()
        {
            return _context.TodoLists.ToList();
        }

        public TodoList GetTodoList(int id)
        {
            return _context.TodoLists.Where(e => e.Id == id).FirstOrDefault();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTodoList(TodoList todoList)
        {
            _context.Update(todoList);
            return Save();
        }
    }
}
