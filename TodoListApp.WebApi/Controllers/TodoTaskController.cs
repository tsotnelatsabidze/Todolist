using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;

namespace TodoListApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskService todoTaskRepository;
        private readonly IMapper mapper;

        public TodoTaskController(ITodoTaskService todoTaskRepository, IMapper mapper)
        {
            this.todoTaskRepository = todoTaskRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoTask>))]
        public IActionResult GetTodoTasks()
        {
            var todoTasks = this.mapper.Map<List<TodoTask>>(this.todoTaskRepository.GetTodoTasks());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok(todoTasks);
        }

        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(TodoTask))]
        [ProducesResponseType(400)]
        public IActionResult GetTodoTask(int taskId)
        {
            if (!this.todoTaskRepository.TodoTaskExists(taskId))
            {
                return this.NotFound();
            }

            var todoTask = this.mapper.Map<TodoTask>(this.todoTaskRepository.GetTodoTask(taskId));

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok(todoTask);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodoTask([FromQuery] int todoListId, [FromQuery] int todoTaskId, [FromBody] TodoTask todoTaskCreate)
        {
            if (todoTaskCreate == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var todoTasks = this.todoTaskRepository.GetTodoTaskTrimToUpper(todoTaskCreate);

            if (todoTasks != null)
            {
                this.ModelState.AddModelError(string.Empty, "Task already exists");
                return this.StatusCode(422, this.ModelState);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var todoTaskMap = this.mapper.Map<TodoTask>(todoTaskCreate);

            if (!this.todoTaskRepository.CreateTodoTask(todoListId, todoTaskId, todoTaskMap))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong while saving");
                return this.StatusCode(500, this.ModelState);
            }

            return this.Ok("Successfully created");
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodoTask(
            int taskId,
            [FromQuery] int ownerId,
            [FromQuery] int listId,
            [FromBody] TodoTask updatedTodoTask)
        {
            if (updatedTodoTask == null)
            {
                return this.BadRequest(this.ModelState);
            }

            if (taskId != updatedTodoTask.Id)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.todoTaskRepository.TodoTaskExists(taskId))
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var todoTaskMap = this.mapper.Map<TodoTask>(updatedTodoTask);

            if (!this.todoTaskRepository.UpdateTodoTask(ownerId, listId, todoTaskMap))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong updating owner");
                return this.StatusCode(500, this.ModelState);
            }

            return this.NoContent();
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodoTask(int taskId)
        {
            if (!this.todoTaskRepository.TodoTaskExists(taskId))
            {
                return this.NotFound();
            }

            var todoTaskToDelete = this.todoTaskRepository.GetTodoTask(taskId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.todoTaskRepository.DeleteTodoTask(todoTaskToDelete))
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong deleting owner");
            }

            return this.NoContent();
        }
    }
}
