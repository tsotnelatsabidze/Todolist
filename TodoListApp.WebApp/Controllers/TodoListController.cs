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
            _logger = logger;
            TodoListWebApiService = todoListWebApiService;
        }

        public IActionResult Index(int? selectedTodoListId)
        {

            List<TodoList> todoLists = TodoListWebApiService.GetTodoLists();
            this.ViewBag.SelectedTodoListId = selectedTodoListId;

            if (selectedTodoListId != null)
            {
                List<TodoTask> todoTasks = TodoListWebApiService.GetToDoTasksByToDoList(selectedTodoListId.Value).ToList();
                return View("Index", new Tuple<List<TodoList>, List<TodoTask>>(todoLists, todoTasks));
            }

            return View("Index", new Tuple<List<TodoList>, List<TodoTask>>(todoLists, new List<TodoTask>()));
        }

        [HttpGet]
        public IActionResult GetTodoListDetails(int todoListId)
        {
            var todoList = TodoListWebApiService.GetTodoListDetails(todoListId);
            return Ok(todoList);
        }

        public IActionResult GetTasks(int todoListId)
        {
            var todoTasks = TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return Ok(todoTasks);
        }

        [HttpPost]
        public IActionResult AddOrUpdateTodoList(int? id, string name, string description)
        {
            if (id == null)
            {
                _ = TodoListWebApiService.AddNew(new TodoListCreateDTO()
                {
                    Name = name,
                    Description = description
                });
            }
            else
            {
                _ = TodoListWebApiService.UpdateToDoList(id.Value, new TodoListUpdateDTO()
                {
                    Name = name,
                    Description = description
                });
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddTodoTask(string title, string description, DateTime dueDate, int selectedTodoListId)
        {
            try
            {
                // Create a new TodoTask with user-provided values
                var newTodoTask = new TodoTaskCreateDTO
                {
                    CreatorUserId = 1,  // You may replace this with the actual user ID
                    Description = description,
                    Title = title,
                    DueDate = dueDate,
                    TodoListId = selectedTodoListId
                };

                TodoListWebApiService.AddNewTask(newTodoTask);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log, return error status, etc.)
                Console.WriteLine($"Error adding new todo task: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteTodoList(int todoListId)
        {
            try
            {
                TodoListWebApiService.DeleteTodoList(todoListId);

                return Json(new { success = true, message = "TodoList deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting todo list: " + ex.Message });
            }
        }
    }
}
