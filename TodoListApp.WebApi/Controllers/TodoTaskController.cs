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
            TodoTaskService = todoTaskService;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateTodoTask")]
        public ActionResult<TodoTask> CreateTodoTask(TodoTaskCreateDTO todoTaskDTO)
        {
            var todoTaskEntity = _mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
            var createdTodoTask = _mapper.Map<TodoTask>(TodoTaskService.CreateTodoTask(todoTaskEntity));
            return Ok(createdTodoTask);
        }


        [HttpGet]
        [Route("GetToDoTasksByTodoListId")]
        public ActionResult<IList<TodoTask>> GetToDoTasksByTodoListId(int Id)
        {
            var todoTasks = _mapper.Map<IList<Services.Models.TodoTask>, IList<TodoTask>>(TodoTaskService.GetTodoTasksByTodoList(Id));
            return Ok(todoTasks);
        }


        [HttpGet("{Id}", Name = "GetTodoTaskById")]
        public ActionResult<TodoTask> GetTodoTaskById(int Id)
        {
            var todoTask = _mapper.Map<TodoTask>(TodoTaskService.GetTodoTask(Id));
            return Ok(todoTask);
        }

        [HttpDelete("{Id}", Name = "DeleteTodoTask")]
        public ActionResult DeleteTodoTask(int Id)
        {
            try
            {
                TodoTaskService.DeleteTodoTask(Id);
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
        public ActionResult<TodoTask> UpdateTodoTask(int Id, TodoTaskUpdateDTO todoTaskDTO)
        {
            try
            {
                var todoTaskEntity = _mapper.Map<Services.Models.TodoTask>(todoTaskDTO);
                var result = TodoTaskService.UpdateTodoTask(Id, todoTaskEntity);
                var updatedTodoTask = _mapper.Map<TodoTask>(result);
                return Ok(updatedTodoTask);
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
