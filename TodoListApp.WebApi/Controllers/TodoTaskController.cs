using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoTaskController : ODataController
    {
        public ITodoTaskService TodoTaskService { get; set; }

        public ITodoTaskReposiotry TodoTaskReposiotry { get; set; }

        private readonly IMapper _mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper, ITodoTaskReposiotry todoTaskReposiotry)
        {
            TodoTaskService = todoTaskService;
            _mapper = mapper;
            TodoTaskReposiotry = todoTaskReposiotry;
        }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTask> CreateTodoTask(TodoTaskCreateDto todoTaskDTO)
        {
            var todoTaskEntity = this._mapper.Map<TodoTask>(todoTaskDTO);
            var createdTodoTask = this._mapper.Map<TodoTask>(this.TodoTaskService.CreateTodoTask(todoTaskEntity));
            return this.Ok(createdTodoTask);
        }

        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTask> GetTodoTaskById(int Id)
        {
            return Ok(this.TodoTaskReposiotry.GetById(Id));
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int Id)
        {
            try
            {
                this.TodoTaskReposiotry.Delete(Id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpPut("{Id}", Name = "UpdateTodoTask")]
        public ActionResult<TodoTask> UpdateTodoTask(int Id, TodoTaskUpdateDto todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = this._mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = this.TodoTaskService.UpdateTodoTask(Id, todoTaskEntity);
                return Ok(result);
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
            return Ok(this.TodoTaskReposiotry.GetAll());
        }
    }
}
