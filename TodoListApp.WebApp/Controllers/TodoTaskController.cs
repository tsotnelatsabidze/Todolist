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
        public TodoTasksWebApiService TodoTasksWebApiService { get; set; }
        public CommentsWebApiService CommentsWebApiService { get; set; }
        public TagWebApiService TagWebApiService { get; set; }

        public TodoTaskController(TodoListWebApiService todoListWebApiService, CommentsWebApiService commentsWebApiService, TodoTasksWebApiService todoTasksWebApiService, TagWebApiService tagWebApiService)
        {
            this.TodoListWebApiService = todoListWebApiService;
            this.CommentsWebApiService = commentsWebApiService;
            this.TodoTasksWebApiService = todoTasksWebApiService;
            this.TagWebApiService = tagWebApiService;
        }

        public IActionResult Index(int todoListId)
        {
            var todoTasks = this.TodoTasksWebApiService.GetToDoTasksByToDoList(todoListId);
            return this.View("Index", todoTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todoTask = await this.TodoTasksWebApiService.GetTodoTaskById(id);

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
                var todoInDb = await this.TodoTasksWebApiService.GetTodoTaskById(id);
                updatedTask.Tags = todoInDb.Tags;
                _ = await this.TodoTasksWebApiService.UpdateTodoTask(id, updatedTask);
                return this.RedirectToAction("TodoTasks", "TodoList", new { id = updatedTask.TodoListId }); // Redirect to the Todos List view
            }

            // If validation fails, redisplay the edit view with validation errors
            return this.View("Index", updatedTask);
        }

        private async Task<ActionResult> GetTodoTaskOrRedirect(int id)
        {
            var response = await this.TodoTasksWebApiService.GetTodoTaskById(id);
            if (response is not null)
            {
                return this.View(response);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return await this.GetTodoTaskOrRedirect(id);
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await this.GetTodoTaskOrRedirect(id);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(TodoTaskDto todoTask)
        {
            await this.TodoTasksWebApiService.DeleteTodoTask(todoTask.Id);
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
                _ = await this.TodoTasksWebApiService.AddNewTaskAsync(todoTask);
                return this.RedirectToAction("TodoTasks", "TodoList", new { id = todoTask.TodoListId });
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions appropriately (log, return error status, etc.)
                Console.WriteLine($"Error adding new todo task: {ex.Message}");
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task AddTag(int todoTaskId, string tag)
        {
            await this.TagWebApiService.AddTagToTodoTask(todoTaskId, tag);
        }

        [HttpPost]
        public async Task RemoveTag(int todoTaskId, string tag)
        {
            await this.TagWebApiService.RemoveTagFromTodoTask(todoTaskId, tag);
        }

        [HttpPost]
        public async Task<CommentDto> AddComment(int todoTaskId, string comment)
        {
            var newComment = await this.CommentsWebApiService.CreateNewComment(new CommentDto()
            {
                CreateDate = DateTime.Now,
                Creator = this.User.GetUserId(),
                Text = comment,
                TodoTaskId = todoTaskId
            });

            return newComment;
        }


        [HttpPost]
        public async Task RemoveComment(int commentId)
        {
            await this.CommentsWebApiService.DeleteComment(commentId);
        }

        [HttpGet]
        public IActionResult TodoTasksByTag(int tagId)
        {
            var tasks = this.TodoTasksWebApiService.GetToDoTasksByTag(tagId);
            return this.View("TodoTasks", tasks);
        }
    }
}
