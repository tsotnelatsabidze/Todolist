using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApi.Models.Models;
using TodoListApp.WebApp.Extensions;

namespace TodoListApp.WebApp.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        public TodoListWebApiService TodoListWebApiService { get; set; }

        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ILogger<TodoListController> logger, TodoListWebApiService todoListWebApiService, UserManager<IdentityUser> userManager)
        {
            this._logger = logger;
            this.TodoListWebApiService = todoListWebApiService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var todoLists = await this.TodoListWebApiService.GetTodoListForUser(this.User.GetUserId());
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
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoListDto todoList)
        {
            if (todoList is null)
            {
                return this.RedirectToAction("Edit", new { todoList.Id });
            }

            var reqResponse = await TodoListWebApiService.Update(id, todoList);
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
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

        public async Task<ActionResult> Delete(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoListDto todoList)
        {
            if (todoList != null)
            {
                if (todoList.Id == id)
                {
                    _ = await this.TodoListWebApiService.Delete(id);
                }
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
            var todoTasks = this.TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return this.Ok(todoTasks);
        }
    }
}
