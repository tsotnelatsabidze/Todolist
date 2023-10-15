using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Services.Database.Entities;

[ApiController]
[Route("api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ITodoListService _todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        _todoListService = todoListService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodoLists()
    {
        var todoLists = await _todoListService.GetAll();
        return Ok(todoLists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoList(int id)
    {
        var todoList = await _todoListService.Get(id);
        if (todoList == null)
        {
            return NotFound($"A to-do list with the id '{id}' was not found.");
        }

        return Ok(todoList);
    }

    [HttpPost]
    public async Task<IActionResult> AddTodoList(TodoListEntity todoList)
    {
        var addedTodoList = await _todoListService.Add(todoList);
        return CreatedAtAction(nameof(GetTodoList), new { id = addedTodoList.Id }, addedTodoList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoList(int id, TodoListEntity todoList)
    {
        await _todoListService.Update(id, todoList);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoList(int id)
    {
        await _todoListService.Delete(id);
        return NoContent();
    }
}
