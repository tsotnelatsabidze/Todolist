using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;

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
        try
        {
            var todoLists = await _todoListService.GetTodoListsAsync();
            return Ok(todoLists);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the to-do lists.");
        }
    }

    [HttpGet("{title}")]
    public async Task<IActionResult> GetTodoListByTitle(string title)
    {
        try
        {
            var todoList = await _todoListService.GetTodoListByTitleAsync(title);
            if (todoList == null)
            {
                return NotFound($"A to-do list with the title '{title}' was not found.");
            }

            return Ok(todoList);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the to-do list.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTodoList(TodoList todoList)
    {
        try
        {
            var addedTodoList = await _todoListService.AddTodoList(todoList);
            return CreatedAtAction(nameof(GetTodoListByTitle), new { title = addedTodoList.Title }, addedTodoList);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding the to-do list.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodoList(TodoList todoList)
    {
        try
        {
            await _todoListService.UpdateTodoListAsync(todoList);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the to-do list.");
        }
    }

    [HttpDelete("{title}")]
    public async Task<IActionResult> DeleteTodoList(string title)
    {
        try
        {
            await _todoListService.DeleteTodoListAsync(title);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the to-do list.");
        }
    }

}
