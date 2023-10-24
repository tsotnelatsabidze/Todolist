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
        public ITodoTaskService TodoTaskService { get; set; }

        private readonly IMapper _mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper)
        {
            this.TodoTaskService = todoTaskService;
            this._mapper = mapper;
        }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTask> CreateTodoTask(TodoTaskCreateDTO todoTaskDTO)
        {
            var todoTaskEntity = this._mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
            var createdTodoTask = _mapper.Map<TodoTask>(this.TodoTaskService.CreateTodoTask(todoTaskEntity));
            return this.Ok(createdTodoTask);
        }

        [HttpGet]
        [Route("GetToDoTasksByTodoListId")]
        public ActionResult<IList<TodoTask>> GetToDoTasksByTodoListId(int Id)
        {
            var todoTasks = _mapper.Map<IList<Services.Models.TodoTask>, IList<TodoTask>>(this.TodoTaskService.GetTodoTasksByTodoList(Id));
            return this.Ok(todoTasks);
        }


        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTask> GetTodoTaskById(int Id)
        {
            var todoTask = this._mapper.Map<TodoTask>(this.TodoTaskService.GetTodoTask(Id));
            return this.Ok(todoTask);
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int Id)
        {
            try
            {
                this.TodoTaskService.DeleteTodoTask(Id);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }

            return this.Ok();
        }


        [HttpPut("{Id}", Name = "UpdateTodoTask")]
        public ActionResult<TodoTask> UpdateTodoTask(int Id, TodoTaskUpdateDTO todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = _mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = TodoTaskService.UpdateTodoTask(Id, todoTaskEntity);
                var updatedTodoTask = _mapper.Map<TodoTask>(result);
                return this.Ok(updatedTodoTask);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            //var todoTasks = _mapper.Map<IQueryable<Services.Models.TodoTask>, IList<TodoTask>>(TodoTaskService.GetAllTodoTasks());
            return Ok(TodoTaskService.GetAllTodoTasks());
        }
    }
}
