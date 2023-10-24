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
            this.TodoListWebApiService = todoListWebApiService;
        }


        public IActionResult Edit(int taskId)
        {
            TodoTask todoTask = this.TodoListWebApiService.GetTodoTaskById(taskId);

            if (todoTask == null)
            {
                return this.NotFound(); // Handle not found task
            }

            return this.View(todoTask);
        }

        [HttpPost]
        public IActionResult Update(int id, TodoTaskUpdateDTO updatedTask)
        {
            // Validate and update the task in your repository
            if (this.ModelState.IsValid)
            {
                _ = this.TodoListWebApiService.UpdateTodoTask(id, updatedTask);
                return this.RedirectToAction("Index", "TodoList"); // Redirect to the TodList  view
            }

            // If validation fails, redisplay the edit view with validation errors
            return this.View(updatedTask);
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            this.TodoListWebApiService.DeleteTodoTask(taskId);

            // If validation fails, redisplay the edit view with validation errors
            return this.Ok();
        }

    }
}
