using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : Controller
    {
        private readonly ITodoListService todoListRepository;
        private readonly IMapper mapper;

        public TodoListController(ITodoListService todoListRepository, IMapper mapper)
        {
            this.todoListRepository = todoListRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoList>))]
        public IActionResult GetTodoLists()
        {
            var todoLists = this.mapper.Map<List<TodoList>>(this.todoListRepository.GetTodoLists());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok(todoLists);
        }

        [HttpGet("{todoListId}")]
        [ProducesResponseType(200, Type = typeof(TodoList))]
        [ProducesResponseType(400)]
        public IActionResult GetTodoList(int todoListId)
        {
            if (!this.todoListRepository.TodoListExists(todoListId))
            {
                return this.NotFound();
            }

            var todoList = this.mapper.Map<TodoList>(this.todoListRepository.GetTodoList(todoListId));

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok(todoList);
        }

        [HttpGet("todoTask/{todoListId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoTask>))]
        [ProducesResponseType(400)]
        public IActionResult GetTodoTasksByTodoListId(int todoListId)
        {
            var todoTasks = this.mapper.Map<List<TodoTask>>(
                this.todoListRepository.GetTodoTasksByTodoList(todoListId));

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            return this.Ok(todoTasks);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodoList([FromBody] TodoList todoListCreate)
        {
            if (todoListCreate == null)
            {
                return this.BadRequest(this.ModelState);
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var todoList = this.todoListRepository.GetTodoLists()
.FirstOrDefault(t => t.Title.Trim().ToUpper() == todoListCreate.Title.TrimEnd().ToUpper());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (todoList != null)
            {
                this.ModelState.AddModelError(string.Empty, "Todo List already exists");
                return this.StatusCode(422, this.ModelState);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var todoListMap = this.mapper.Map<TodoList>(todoListCreate);

            if (!this.todoListRepository.CreateTodoList(todoListMap))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong while saving");
                return this.StatusCode(500, this.ModelState);
            }

            return this.Ok("Successfully created");
        }

        [HttpPut("{todoTaskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodoTask(int todoTaskId, [FromBody] TodoTask updatedTodoTask)
        {
            if (updatedTodoTask == null)
            {
                return this.BadRequest(this.ModelState);
            }

            if (todoTaskId != updatedTodoTask.Id)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.todoListRepository.TodoListExists(todoTaskId))
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var todoListMap = this.mapper.Map<TodoList>(updatedTodoTask);

            if (!this.todoListRepository.UpdateTodoList(todoListMap))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong updating Todo List");
                return this.StatusCode(500, this.ModelState);
            }

            return this.NoContent();
        }

        [HttpDelete("{todoListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodoList(int todoListId)
        {
            if (!this.todoListRepository.TodoListExists(todoListId))
            {
                return this.NotFound();
            }

            var todoListToDelete = this.todoListRepository.GetTodoList(todoListId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.todoListRepository.DeleteTodoList(todoListToDelete))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong deleting Todo List");
            }

            return this.NoContent();
        }
    }
}
