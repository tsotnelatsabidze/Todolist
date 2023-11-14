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

        public ITodoTaskRepository TodoTaskReposiotry { get; set; }

        private readonly IMapper _mapper;

        public TodoTaskController(ITodoTaskService todoTaskService, IMapper mapper, ITodoTaskRepository todoTaskReposiotry)
        {
            TodoTaskService = todoTaskService;
            _mapper = mapper;
            TodoTaskReposiotry = todoTaskReposiotry;
        }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTaskDto> CreateTodoTask(TodoTaskCreateDto todoTaskDTO)
        {
            var todoTaskEntity = _mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
            var createdTodoTask = _mapper.Map<TodoTaskDto>(TodoTaskService.CreateTodoTask(todoTaskEntity));
            return Ok(createdTodoTask);
        }



        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTaskDto> GetTodoTaskById(int Id)
        {
            return Ok(TodoTaskReposiotry.GetById(Id));
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int Id)
        {
            try
            {
                TodoTaskReposiotry.Delete(Id);
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
        public ActionResult<TodoTaskDto> UpdateTodoTask(int Id, TodoTaskUpdateDto todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = _mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = TodoTaskService.UpdateTodoTask(Id, todoTaskEntity);
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
            return Ok(TodoTaskReposiotry.GetAll());
        }
    }
}
