using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApp.Extensions;
using TodoListApp.Services.Models;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApp.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        public TodoListWebApiService TodoListWebApiService { get; set; }

        public TodoTasksWebApiService TodoTasksWebApiService { get; set; }

#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable S4487 // Unread "private" fields should be removed
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<TodoListController> _logger;
#pragma warning restore S4487 // Unread "private" fields should be removed
#pragma warning restore IDE0052 // Remove unread private members


        public TodoListController(ILogger<TodoListController> logger, TodoListWebApiService todoListWebApiService, UserManager<IdentityUser> userManager, TodoTasksWebApiService todoTasksWebApiService)
        {
            this._logger = logger;
            this.TodoListWebApiService = todoListWebApiService;
            this._userManager = userManager;
            this.TodoTasksWebApiService = todoTasksWebApiService;
        }

        public async Task<IActionResult> Index()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var todoLists = await this.TodoListWebApiService.GetTodoListForUser(this.User.GetUserId());
#pragma warning restore CS8604 // Possible null reference argument.
            return this.View("Index", todoLists);
        }

        public async Task<IActionResult> TodoTasks(int id)
        {
            var todoList = await this.TodoListWebApiService.GetTodoListDetails(id);
            return this.View("TodoTasks", todoList);
        }

        /// <summary>
        /// Load Page to Create A New TodoList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Save New Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(TodoListCreateDto? todoList)
        {
            if (todoList is not null)
            {
                todoList.CreatorUserId = this.User.GetUserId();
                _ = await this.TodoListWebApiService.AddNewAsync(todoList);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Edit an employee by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<ActionResult> GetTodoListOrRedirect(int id)
        {
            var response = await this.TodoListWebApiService.GetTodoList(id);
            if (response is not null)
            {
                return this.View(response);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            return await this.GetTodoListOrRedirect(id);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await this.GetTodoListOrRedirect(id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoListDto todoList)
        {
            if (todoList is null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable S2259 // Null pointers should not be dereferenced
                return this.RedirectToAction("Edit", new { todoList.Id });
#pragma warning restore S2259 // Null pointers should not be dereferenced
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            _ = await this.TodoListWebApiService.Update(id, todoList);
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            return await this.GetTodoListOrRedirect(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoList todoList)
        {
            if (todoList != null && todoList.Id == id)
            {
                _ = await this.TodoListWebApiService.Delete(id);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetTodoListDetails(int todoListId)
        {
            var todoList = this.TodoListWebApiService.GetTodoList(todoListId);
            return this.Ok(todoList);
        }

        public IActionResult GetTasks(int todoListId)
        {
            var todoTasks = this.TodoTasksWebApiService.GetToDoTasksByToDoList(todoListId);
            return this.Ok(todoTasks);
        }
    }
}
