namespace TodoListApp.Services.Database
{
    public class TodoListDatabaseService : ITodoListService
    {
        private readonly DataContext context;

        public TodoListDatabaseService(DataContext context)
        {
            this.context = context;
        }

        public bool TodoListExists(int id)
        {
            return this.context.TodoLists.Any(t => t.Id == id);
        }

        public bool CreateTodoList(TodoList todoList)
        {
            _ = this.context.Add(todoList);
            return this.Save();
        }

        public bool DeleteTodoList(TodoList todoList)
        {
            _ = this.context.Remove(todoList);
            return this.Save();
        }

        public ICollection<TodoList> GetTodoLists()
        {
            return this.context.TodoLists.Select(t => new TodoList
            {
                Id = t.Id,
            }).ToList();
        }

        public TodoList GetTodoList(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return this.context.TodoLists.Where(e => e.Id == id).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<TodoTask> GetTodoTasksByTodoList(int todoListId)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return this.context.TodoTaskTodoLists.Where(e => e.TodoListId == todoListId).Select(t => t.TodoTask).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTodoList(TodoList todoList)
        {
            _ = this.context.Update(todoList);
            return this.Save();
        }
    }
}
