using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskService _todoTaskService;
        private readonly IMapper _mapper;
        public TodoTaskController(ITodoTaskService todoTaskRepository, IMapper mapper)
        {
            _todoTaskService = todoTaskRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoTask>))]
        public IActionResult GetTodoTasks()
        {
            var todoTasks = _mapper.Map<List<TodoTask>>(_todoTaskService.GetTodoTasks());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(todoTasks);
        }

        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(TodoTask))]
        [ProducesResponseType(400)]
        public IActionResult GetTodoTask(int taskId)
        {
            if (!_todoTaskService.TodoTaskExists(taskId))
            {
                return NotFound();
            }

            var todoTask = _mapper.Map<TodoTask>(_todoTaskService.GetTodoTask(taskId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(todoTask);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodoTask([FromBody] TodoTask todoTaskCreate)
        {
            if (todoTaskCreate == null)
            {
                return BadRequest(ModelState);
            }

            var todoTasks = _todoTaskService.GetTodoTaskTrimToUpper(todoTaskCreate);

            if (todoTasks != null)
            {
                ModelState.AddModelError("", "Task already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoTaskMap = _mapper.Map<TodoTask>(todoTaskCreate);


            if (!_todoTaskService.CreateTodoTask(todoTaskMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodoTask(int taskId,
            [FromQuery] int ownerId, [FromQuery] int listId,
            [FromBody] TodoTask updatedTodoTask)
        {
            if (updatedTodoTask == null)
            {
                return BadRequest(ModelState);
            }

            if (taskId != updatedTodoTask.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_todoTaskService.TodoTaskExists(taskId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todoTaskMap = _mapper.Map<TodoTask>(updatedTodoTask);

            if (!_todoTaskService.UpdateTodoTask(ownerId, listId, todoTaskMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodoTask(int taskId)
        {
            if (!_todoTaskService.TodoTaskExists(taskId))
            {
                return NotFound();
            }

            var todoTaskToDelete = _todoTaskService.GetTodoTask(taskId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_todoTaskService.DeleteTodoTask(todoTaskToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
