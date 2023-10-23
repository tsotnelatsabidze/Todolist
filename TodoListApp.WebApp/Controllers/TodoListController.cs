using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.Models;
using TodoListApp.Services.WebApi;

namespace TodoListApp.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoListWebApiService todoListService;

        public TodoListController(TodoListWebApiService todoListService)
        {
            this.todoListService = todoListService;
        }


        // GET: api/TodoList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
            var todoLists = await this.todoListService.GetTodoListsAsync();
            return this.Ok(todoLists);
        }

        // GET: api/TodoList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
            var todoList = await this.todoListService.GetTodoListAsync(id);

            if (todoList == null)
            {
                return this.NotFound();
            }

            return todoList;
        }

        // POST: api/TodoList
        [HttpPost]
        public async Task<ActionResult<TodoList>> CreateTodoList(TodoList todoList)
        {
            var created = await this.todoListService.CreateTodoListAsync(todoList);

            if (!created)
            {
                return this.BadRequest();
            }

            return this.CreatedAtAction(nameof(this.GetTodoList), new { id = todoList.Id }, todoList);
        }

        // PUT: api/TodoList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoList(int id, TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return this.BadRequest();
            }

            var updated = await this.todoListService.UpdateTodoListAsync(todoList);

            if (!updated)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var deleted = await this.todoListService.DeleteTodoListAsync(id);

            if (!deleted)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
