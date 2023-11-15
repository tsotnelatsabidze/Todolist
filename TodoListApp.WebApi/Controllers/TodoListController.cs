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
        private readonly IMapper mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper, ITodoListRepository todoListRepository)
        {
            this.TodoListService = todoListService;
            this.mapper = mapper;
            this.TodoListRepository = todoListRepository;
        }

        public ITodoListService TodoListService { get; set; }

        public ITodoListRepository TodoListRepository { get; set; }

        [EnableQuery]
        [HttpGet(Name = "GetToDoLists")]
        public ActionResult<TodoListDto> GetToDoLists()
        {
            var todoList = this.TodoListRepository.GetAll();
            return this.Ok(todoList);
        }

        [EnableQuery]
        [HttpGet("{todoListId}", Name = "GetToDoList")]
        public ActionResult<TodoListDto> GetToDoList(int todoListId)
        {
            var todoList = this.TodoListRepository.GetById(todoListId);
            return this.Ok(todoList);
        }

        [HttpPost(Name = "CreateToDoList")]
        public ActionResult<TodoListDto> CreateToDoList([FromBody] TodoListCreateDto todoList)
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
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }

            return this.Ok();
        }

        [HttpPut("{id}", Name = "UpdateToDoList")]
        public ActionResult<TodoListDto> UpdateToDoList(int id, [FromBody] TodoListUpdateDto todoList)
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
            catch (InvalidOperationException)
            {
                return this.StatusCode(500);
            }
        }
    }
}
