using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        public ITodoListService TodoListService { get; set; }

        public ITodoListRepository TodoListRepository { get; set; }

        private readonly IMapper mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper, ITodoListRepository todoListRepository)
        {
            this.TodoListService = todoListService;
            this.mapper = mapper;
            this.TodoListRepository = todoListRepository;
        }

        [EnableQuery]
        [HttpGet(Name = "GetToDoLists")]
        public ActionResult<TodoList> GetToDoLists()
        {
            var todoList = this.TodoListRepository.GetAll();
            return this.Ok(todoList);
        }

        [EnableQuery]
        [HttpGet("{todoListId}", Name = "GetToDoList")]
        public ActionResult<TodoList> GetToDoList(int todoListId)
        {
            var todoList = this.TodoListRepository.GetById(todoListId);
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
            catch (Exception)
            {
                return this.StatusCode(500);
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
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }
    }
}
