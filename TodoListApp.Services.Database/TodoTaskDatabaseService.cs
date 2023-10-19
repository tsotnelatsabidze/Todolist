namespace TodoListApp.Services.Database
{
    public class TodoTaskDatabaseService : ITodoTaskService
    {
        private readonly DataContext context;

        public TodoTaskDatabaseService(DataContext context)
        {
            this.context = context;
        }

        public bool CreateTodoTask(int ownerId, int todoListId, TodoTask todoTask)
        {
            var todoList = this.context.TodoLists.Where(a => a.Id == todoListId).FirstOrDefault();

            var todoTaskTodoList = new TodoTaskTodoList()
            {
                TodoList = todoList,
                TodoTask = todoTask,
            };

            _ = this.context.Add(todoTaskTodoList);

            _ = this.context.Add(todoTask);

            return this.Save();
        }

        public bool DeleteTodoTask(TodoTask todoTask)
        {
            _ = this.context.Remove(todoTask);
            return this.Save();
        }

        public TodoTask GetTodoTask(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return this.context.TodoTasks.Where(t => t.Id == id).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TodoTask GetTodoTask(string title)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return this.context.TodoTasks.Where(t => t.Title == title).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<TodoTask> GetTodoTasks()
        {
            return this.context.TodoTasks.ToList();
        }

        public TodoTask GetTodoTaskTrimToUpper(TodoTask todoTaskCreate)
        {
#pragma warning disable CS8603,CS8602 // Possible null reference return.
            return this.GetTodoTasks().FirstOrDefault(c => c.Title.Trim().ToUpper() == todoTaskCreate.Title.TrimEnd().ToUpper());
#pragma warning restore CS8603,CS8602 // Possible null reference return.
        }

        public bool TodoTaskExists(int taskId)
        {
            return this.context.TodoTasks.Any(t => t.Id == taskId);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTodoTask(int ownerId, int todoListId, TodoTask todoTask)
        {
            _ = this.context.Update(todoTask);
            return this.Save();
        }
    }
}
