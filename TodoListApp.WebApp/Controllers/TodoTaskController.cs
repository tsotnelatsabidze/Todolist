using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApp.Controllers
{
    public class TodoTaskController : Controller
    {

        public TodoListWebApiService TodoListWebApiService { get; set; }

        public TodoTaskController(TodoListWebApiService todoListWebApiService)
        {
            TodoListWebApiService = todoListWebApiService;
        }


        public IActionResult Edit(int taskId)
        {
            TodoTask todoTask = TodoListWebApiService.GetTodoTaskById(taskId);

            if (todoTask == null)
            {
                return NotFound(); // Handle not found task
            }

            return View(todoTask);
        }

        [HttpPost]
        public IActionResult Update(int id, TodoTaskUpdateDTO updatedTask)
        {
            // Validate and update the task in your repository
            if (ModelState.IsValid)
            {
                TodoListWebApiService.UpdateTodoTask(id, updatedTask);
                return RedirectToAction("Index", "TodoList"); // Redirect to the Todo List view
            }

            // If validation fails, redisplay the edit view with validation errors
            return View(updatedTask);
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            TodoListWebApiService.DeleteTodoTask(taskId);

            // If validation fails, redisplay the edit view with validation errors
            return Ok();
        }

    }
}
