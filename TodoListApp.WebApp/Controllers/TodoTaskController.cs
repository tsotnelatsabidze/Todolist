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
            TodoListWebApiService = todoListWebApiService;
        }

        public IActionResult Index(int todoListId)
        {
            var todoTasks = TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return View("Index", todoTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todoTask = await TodoListWebApiService.GetTodoTaskById(id);

            if (todoTask == null)
            {
                return NotFound(); // Handle not found task
            }

            return View(todoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoTaskUpdateDto updatedTask)
        {
            // Validate and update the task in your repository
            if (ModelState.IsValid)
            {
                var todoInDb = await TodoListWebApiService.GetTodoTaskById(id);
                updatedTask.Tags = todoInDb.Tags;
                await TodoListWebApiService.UpdateTodoTask(id, updatedTask);
                return RedirectToAction("TodoTasks", "TodoList", new { id = updatedTask.TodoListId }); // Redirect to the Todo List view
            }

            // If validation fails, redisplay the edit view with validation errors
            return View("Index", updatedTask);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await TodoListWebApiService.GetTodoTaskById(id);

            if (response is not null)
            {
                return View(response);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TodoTask todoTask)
        {
            await TodoListWebApiService.DeleteTodoTask(todoTask.Id);
            return RedirectToAction("TodoTasks", "TodoList", new { id = todoTask.TodoListId });
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
                return RedirectToAction("TodoTasks", "TodoList", new { id = todoTask.TodoListId });
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions appropriately (log, return error status, etc.)
                Console.WriteLine($"Error adding new todo task: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = await TodoListWebApiService.GetTodoTaskById(id);
            if (response is not null)
            {
                return View(response);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task AddTag(int todoTaskId, string tag)
        {
            await TodoListWebApiService.AddTagToTodoTask(todoTaskId, tag);
        }

        [HttpPost]
        public async Task RemoveTag(int todoTaskId, string tag)
        {
            await TodoListWebApiService.RemoveTagFromTodoTask(todoTaskId, tag);
        }
    }
}
