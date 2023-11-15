using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoTaskController : ODataController
    {
        private readonly IMapper mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper, ITodoTaskRepository todoTaskReposiotry)
        {
            this.TodoTaskService = todoTaskService;
            this.mapper = mapper;
            this.TodoTaskReposiotry = todoTaskReposiotry;
        }

        public ITodoTaskService TodoTaskService { get; set; }

        public ITodoTaskRepository TodoTaskReposiotry { get; set; }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTaskDto> CreateTodoTask(TodoTaskCreateDto todoTaskDTO)
        {
            var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
            var createdTodoTask = this.mapper.Map<TodoTaskDto>(this.TodoTaskService.CreateTodoTask(todoTaskEntity));
            return this.Ok(createdTodoTask);
        }

        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTaskDto> GetTodoTaskById(int id)
        {
            return this.Ok(this.TodoTaskReposiotry.GetById(id));
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int id)
        {
            try
            {
                this.TodoTaskReposiotry.Delete(id);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPut("{Id}", Name = "UpdateTodoTask")]
        public ActionResult<TodoTaskDto> UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = this.TodoTaskService.UpdateTodoTask(id, todoTaskEntity);
                return this.Ok(result);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return this.Ok(this.TodoTaskReposiotry.GetAll());
        }
    }
}
