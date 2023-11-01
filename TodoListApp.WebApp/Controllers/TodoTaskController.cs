using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApi.Models.Models;
using TodoListApp.WebApp.Extensions;

namespace TodoListApp.WebApp.Controllers
{
    [Authorize]
    public class TodoTaskController : Controller
    {

        public TodoListWebApiService TodoListWebApiService { get; set; }

        public TodoTaskController(TodoListWebApiService todoListWebApiService)
        {
            this.TodoListWebApiService = todoListWebApiService;
        }

        public IActionResult Index(int todoListId)
        {
            var todoTasks = this.TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return this.View("Index", todoTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todoTask = await this.TodoListWebApiService.GetTodoTaskById(id);

            if (todoTask == null)
            {
                return this.NotFound(); // Handle not found task
            }

            return this.View(todoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoTaskUpdateDto updatedTask)
        {
            // Validate and update the task in your repository
            if (this.ModelState.IsValid)
            {
                var todoInDb = await this.TodoListWebApiService.GetTodoTaskById(id);
                updatedTask.Tags = todoInDb.Tags;
                _ = await this.TodoListWebApiService.UpdateTodoTask(id, updatedTask);
                return this.RedirectToAction("TodoTasks", "TodoList", new { id = updatedTask.TodoListId }); // Redirect to the Todos List view
            }

            // If validation fails, redisplay the edit view with validation errors
            return this.View("Index", updatedTask);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await this.TodoListWebApiService.GetTodoTaskById(id);

            if (response is not null)
            {
                return this.View(response);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TodoTaskDto todoTask)
        {
            await this.TodoListWebApiService.DeleteTodoTask(todoTask.Id);
            return this.RedirectToAction("TodoTasks", "TodoList", new { id = todoTask.TodoListId });
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            return this.View(new TodoTaskCreateDto() { TodoListId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoTaskCreateDto todoTask)
        {
            try
            {
                todoTask.CreatorUserId = this.User.GetUserId();
                _ = await this.TodoListWebApiService.AddNewTaskAsync(todoTask);
                return this.RedirectToAction("TodoTasks", "TodoList", new { id = todoTask.TodoListId });
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions appropriately (log, return error status, etc.)
                Console.WriteLine($"Error adding new todo task: {ex.Message}");
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = await this.TodoListWebApiService.GetTodoTaskById(id);
            if (response is not null)
            {
                return this.View(response);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task AddTag(int todoTaskId, string tag)
        {
            await this.TodoListWebApiService.AddTagToTodoTask(todoTaskId, tag);
        }

        [HttpPost]
        public async Task RemoveTag(int todoTaskId, string tag)
        {
            await this.TodoListWebApiService.RemoveTagFromTodoTask(todoTaskId, tag);
        }
    }
}
