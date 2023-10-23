using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : Controller
    {
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper)
        {
            _todoListService = todoListService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoList>))]
        public IActionResult GetTodoLists()
        {
            var todoLists = _mapper.Map<List<TodoList>>(_todoListService.GetTodoLists());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(todoLists);
        }

        [HttpGet("{todoListId}")]
        [ProducesResponseType(200, Type = typeof(TodoList))]
        [ProducesResponseType(400)]
        public IActionResult GetTodoList(int todoListId)
        {
            if (!_todoListService.TodoListExists(todoListId))
            {
                return NotFound();
            }

            var todoList = _mapper.Map<TodoList>(_todoListService.GetTodoList(todoListId));

            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(todoList);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodoList([FromBody] TodoList todoListCreate)
        {
            if (todoListCreate == null)
            {
                return BadRequest(ModelState);
            }

            var todoList = _todoListService.GetTodoLists()
                .Where(t => t.Title.Trim().ToUpper() == todoListCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (todoList != null)
            {
                ModelState.AddModelError("", "Todo List already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoListMap = _mapper.Map<TodoList>(todoListCreate);

            if (!_todoListService.CreateTodoList(todoListMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{todoTaskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodoTask(int todoTaskId, [FromBody] TodoTask updatedTodoTask)
        {
            if (updatedTodoTask == null)
            {
                return BadRequest(ModelState);
            }

            if (todoTaskId != updatedTodoTask.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_todoListService.TodoListExists(todoTaskId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todoListMap = _mapper.Map<TodoList>(updatedTodoTask);

            if (!_todoListService.UpdateTodoList(todoListMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Todo List");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{todoListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodoList(int todoListId)
        {
            if (!_todoListService.TodoListExists(todoListId))
            {
                return NotFound();
            }

            var todoListToDelete = _todoListService.GetTodoList(todoListId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_todoListService.DeleteTodoList(todoListToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Todo List");
            }

            return NoContent();
        }
    }
}
