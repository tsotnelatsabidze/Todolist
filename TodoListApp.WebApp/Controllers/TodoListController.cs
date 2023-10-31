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
            _logger = logger;
            TodoListWebApiService = todoListWebApiService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var todoLists = await TodoListWebApiService.GetTodoListForUser(User.GetUserId());
            return View("Index", todoLists);
        }

        public async Task<IActionResult> TodoTasks(int id)
        {
            var todoList = await TodoListWebApiService.GetTodoListDetails(id);
            return View("TodoTasks", todoList);
        }

        /// <summary>
        /// Load Page to Create A New TodoList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Save New Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(TodoListCreateDTO? todoList)
        {
            if (todoList is not null)
            {
                todoList.CreatorUserId = User.GetUserId();
                var reqResponse = await TodoListWebApiService.AddNewAsync(todoList);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit an employee by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await TodoListWebApiService.GetTodoList(id);
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoList todoList)
        {
            if (todoList is null)
            {
                return RedirectToAction("Edit", new { todoList.Id });
            }

            var reqResponse = await TodoListWebApiService.Update(id, todoList);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var response = await TodoListWebApiService.GetTodoList(id);
            if (response is not null)
            {
                return View(response);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await TodoListWebApiService.GetTodoList(id);

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoList todoList)
        {
            if (todoList != null)
            {
                if (todoList.Id == id)
                {
                    _ = await TodoListWebApiService.Delete(id);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetTodoListDetails(int todoListId)
        {
            var todoList = TodoListWebApiService.GetTodoList(todoListId);
            return Ok(todoList);
        }

        public IActionResult GetTasks(int todoListId)
        {
            var todoTasks = TodoListWebApiService.GetToDoTasksByToDoList(todoListId);
            return Ok(todoTasks);
        }
    }
}
