using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApp.Controllers
{
    public class TodoListController : Controller
    {
        public TodoListWebApiService TodoListWebApiService { get; set; }

        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ILogger<TodoListController> logger, TodoListWebApiService todoListWebApiService)
        {
            this._logger = logger;
            this.TodoListWebApiService = todoListWebApiService;
        }

        public IActionResult Index(int? selectedTodoListId)
        {

            List<TodoList> todoLists = this.TodoListWebApiService.GetTodoLists();
            this.ViewBag.SelectedTodoListId = selectedTodoListId;

            if (selectedTodoListId != null)
            {
                List<TodoTask> todoTasks = this.TodoListWebApiService.GetToDoTasksByToDoList(selectedTodoListId.Value).ToList();
                return this.View("Index", new Tuple<List<TodoList>, List<TodoTask>>(todoLists, todoTasks));
            }

            return this.View("Index", new Tuple<List<TodoList>, List<TodoTask>>(todoLists, new List<TodoTask>()));
        }

        [HttpGet]
        public IActionResult GetTodoListDetails(int todoListId)
        {
            var todoList = this.TodoListWebApiService.GetTodoListDetails(todoListId);
            return this.Ok(todoList);
        }

        public IActionResult GetTasks(int todoListId)
        {
            var todoTasks = this.TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return this.Ok(todoTasks);
        }

        [HttpPost]
        public IActionResult AddOrUpdateTodoList(int? id, string name, string description)
        {
            if (id == null)
            {
                _ = this.TodoListWebApiService.AddNew(new TodoListCreateDto()
                {
                    Name = name,
                    Description = description
                });
            }
            else
            {
                _ = this.TodoListWebApiService.UpdateToDoList(id.Value, new TodoListUpdateDto()
                {
                    Name = name,
                    Description = description
                });
            }

            return this.Ok();
        }

        [HttpPost]
        public IActionResult AddTodoTask(string title, string description, DateTime dueDate, int selectedTodoListId)
        {
            try
            {
                // Create a new TodoTask with user-provided values
                var newTodoTask = new TodoTaskCreateDto
                {
                    CreatorUserId = 1,  // You may replace this with the actual user ID
                    Description = description,
                    Title = title,
                    DueDate = dueDate,
                    TodoListId = selectedTodoListId
                };

                _ = this.TodoListWebApiService.AddNewTask(newTodoTask);
                return this.Ok();
            }
            catch (ArgumentNullException ex)
            {
                // Handle exceptions appropriately (log, return error status, etc.)
                Console.WriteLine($"Error adding new todo task: {ex.Message}");
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteTodoList(int todoListId)
        {
            try
            {
                this.TodoListWebApiService.DeleteTodoList(todoListId);

                return this.Json(new { success = true, message = "TodoList deleted successfully." });
            }
            catch (ArgumentNullException ex)
            {
                return this.Json(new { success = false, message = "Error deleting todo list: " + ex.Message });
            }
        }
    }
}
