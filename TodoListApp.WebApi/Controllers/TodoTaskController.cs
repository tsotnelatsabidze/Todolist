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
        public ITodoTaskService TodoTaskService { get; set; }

        public ITodoTaskReposiotry TodoTaskReposiotry { get; set; }

        private readonly IMapper mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper, ITodoTaskReposiotry todoTaskReposiotry)
        {
            this.TodoTaskService = todoTaskService;
            this.mapper = mapper;
            this.TodoTaskReposiotry = todoTaskReposiotry;
        }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTask> CreateTodoTask(TodoTaskCreateDto todoTaskDto)
        {
            var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDto);
            var createdTodoTask = this.mapper.Map<TodoTask>(this.TodoTaskService.CreateTodoTask(todoTaskEntity));
            return this.Ok(createdTodoTask);
        }



        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTask> GetTodoTaskById(int Id)
        {
            return this.Ok(this.TodoTaskReposiotry.GetById(Id));
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
                return this.NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }

            return this.Ok();
        }


        [HttpPut("{Id}", Name = "UpdateTodoTask")]
        public ActionResult<TodoTask> UpdateTodoTask(int Id, TodoTaskUpdateDto todoTaskDto)
        {
            try
            {
                var todoTaskEntity = this.mapper.Map<Services.Models.TodoTask>(todoTaskDto);
                var result = this.TodoTaskService.UpdateTodoTask(Id, todoTaskEntity);
                return this.Ok(result);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
            catch (Exception)
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
