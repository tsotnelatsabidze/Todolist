using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        public ITagService TagService { get; set; }

        public IMapper Mapper { get; set; }

        public TagController(ITagService tagService, IMapper mapper)
        {
            this.TagService = tagService;
            this.Mapper = mapper;
        }

        [HttpPost(Name = "CreateTag")]
        public ActionResult<TagDto> CreateTag(int todoTaskId, string tag)
        {
            return Ok(TagService.CreateTag(todoTaskId, tag));
        }

        [HttpGet("{Id}", Name = "GetTagById")]
        public ActionResult<TagDto> GetTagById(int Id)
        {
            return Ok(Mapper.Map<TagDto>(TagService.GetTag(Id)));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return Ok(TagService.GetAll());
        }

        [HttpDelete]
        public IActionResult DeleteTagFromTodoTask(int todoTaskId, string tag)
        {
            this.TagService.DeleteTagFromTodoTask(todoTaskId, tag);
            return NoContent();
        }
    }
}
