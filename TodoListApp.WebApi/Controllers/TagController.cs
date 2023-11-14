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
            return this.Ok(this.TagService.CreateTag(todoTaskId, tag));
        }

        [HttpGet("{Id}", Name = "GetTagById")]
        public ActionResult<TagDto> GetTagById(int Id)
        {
            return this.Ok(this.Mapper.Map<TagDto>(this.TagService.GetTag(Id)));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return this.Ok(this.TagService.GetAll());
        }

        [HttpDelete]
        public IActionResult DeleteTagFromTodoTask(int todoTaskId, string tag)
        {
            this.TagService.DeleteTagFromTodoTask(todoTaskId, tag);
            return this.NoContent();
        }
    }
}
