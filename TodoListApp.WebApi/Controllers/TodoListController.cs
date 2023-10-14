using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService service;

        public TodoListController(ITodoListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoLists()
        {
            var todoLists = await this.service.GetTodoListsAsync();
            var todoListModels = todoLists.Select(t => new TodoListModel
            {
                Title = t.Title,
                Description = t.Description,
                NumberOfTasks = t.NumberOfTasks,
                IsShared = t.IsShared,
            }).ToList();

            return this.Ok(todoListModels);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetTodoListByTitle(string title)
        {
            var todoList = await this.service.GetTodoListByTitleAsync(title);
            if (todoList == null)
            {
                return this.NotFound();
            }

            var model = new TodoListModel
            {
                Title = todoList.Title,
                Description = todoList.Description,
                NumberOfTasks = todoList.NumberOfTasks,
                IsShared = todoList.IsShared,
            };

            return this.Ok(model);
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteTodoList(string title)
        {
            await this.service.DeleteTodoListAsync(title);

            return this.NoContent();
        }

        [HttpPut("{title}")]
        public async Task<IActionResult> UpdateTodoList(string title, [FromBody] TodoListModel model)
        {
            var todoList = await this.service.GetTodoListByTitleAsync(title);
            if (todoList == null)
            {
                return this.NotFound();
            }

            todoList.Title = model.Title;
            todoList.Description = model.Description;
            todoList.NumberOfTasks = model.NumberOfTasks;
            todoList.IsShared = model.IsShared;

            await this.service.UpdateTodoListAsync(todoList);

            return this.NoContent();
        }
    }
}
