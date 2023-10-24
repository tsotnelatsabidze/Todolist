using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoTaskController : ODataController
    {
        private readonly IMapper mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper)
        {
            this.TodoTaskService = todoTaskService;
            this.mapper = mapper;
        }

        public ITodoTaskService TodoTaskService { get; set; }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTask> CreateTodoTask(TodoTaskCreateDto todoTaskDTO)
        {
            var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
            var createdTodoTask = this.mapper.Map<TodoTask>(this.TodoTaskService.CreateTodoTask(todoTaskEntity));
            return this.Ok(createdTodoTask);
        }

        [HttpGet]
        [Route("GetToDoTasksByTodoListId")]
        public ActionResult<IList<TodoTask>> GetToDoTasksByTodoListId(int id)
        {
            var todoTasks = this.mapper.Map<IList<Services.Models.TodoTask>, IList<TodoTask>>(this.TodoTaskService.GetTodoTasksByTodoList(id));
            return this.Ok(todoTasks);
        }

        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTask> GetTodoTaskById(int id)
        {
            var todoTask = this.mapper.Map<TodoTask>(this.TodoTaskService.GetTodoTask(id));
            return this.Ok(todoTask);
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int id)
        {
            try
            {
                this.TodoTaskService.DeleteTodoTask(id);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPut("{Id}", Name = "UpdateTodoTask")]
        public ActionResult<TodoTask> UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = this.TodoTaskService.UpdateTodoTask(id, todoTaskEntity);
                var updatedTodoTask = this.mapper.Map<TodoTask>(result);
                return this.Ok(updatedTodoTask);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return this.Ok(this.TodoTaskService.GetAllTodoTasks());
        }
    }
}
