using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        public ITodoListService TodoListService { get; set; }

        private readonly IMapper _mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper)
        {
            TodoListService = todoListService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetToDoLists")]
        public ActionResult<TodoList> GetToDoLists()
        {
            var todoList = TodoListService.GetTodoLists();
            return Ok(todoList);
        }

        [HttpGet("{todoListId}", Name = "GetToDoList")]
        public ActionResult<TodoList> GetToDoList(int todoListId)
        {
            var todoList = TodoListService.GetTodoListById(todoListId);
            return Ok(todoList);
        }


        [HttpPost(Name = "CreateToDoList")]
        public ActionResult<TodoList> CreateToDoList([FromBody] TodoListCreateDTO todoList)
        {
            var result = TodoListService.CreateTodoList(_mapper.Map<Services.Models.TodoList>(todoList));
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteToDoList")]
        public ActionResult DeleteToDoList(int id)
        {
            try
            {
                TodoListService.DeleteTodoList(id);
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

        [HttpPut("{id}", Name = "UpdateToDoList")]
        public ActionResult<TodoList> UpdateToDoList(int id, [FromBody] TodoListUpdateDTO todoList)
        {
            try
            {
                var result = TodoListService.UpdateTodoList(id, _mapper.Map<Services.Models.TodoList>(todoList));
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
    }
}
