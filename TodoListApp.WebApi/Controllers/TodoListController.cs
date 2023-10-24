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
        private readonly IMapper mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper)
        {
            this.TodoListService = todoListService;
            this.mapper = mapper;
        }

        public ITodoListService TodoListService { get; set; }

        [HttpGet(Name = "GetToDoLists")]
        public ActionResult<TodoList> GetToDoLists()
        {
            var todoList = this.TodoListService.GetTodoLists();
            return this.Ok(todoList);
        }

        [HttpGet("{todoListId}", Name = "GetToDoList")]
        public ActionResult<TodoList> GetToDoList(int todoListId)
        {
            var todoList = this.TodoListService.GetTodoListById(todoListId);
            return this.Ok(todoList);
        }

        [HttpPost(Name = "CreateToDoList")]
        public ActionResult<TodoList> CreateToDoList([FromBody] TodoListCreateDto todoList)
        {
            var result = this.TodoListService.CreateTodoList(this.mapper.Map<Services.Models.TodoList>(todoList));
            return this.Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteToDoList")]
        public ActionResult DeleteToDoList(int id)
        {
            try
            {
                this.TodoListService.DeleteTodoList(id);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPut("{id}", Name = "UpdateToDoList")]
        public ActionResult<TodoList> UpdateToDoList(int id, [FromBody] TodoListUpdateDto todoList)
        {
            try
            {
                var result = this.TodoListService.UpdateTodoList(id, this.mapper.Map<Services.Models.TodoList>(todoList));
                return this.Ok(result);
            }
            catch (ArgumentNullException)
            {
                return this.NotFound();
            }
        }
    }
}
