using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        public TagController(ITagReposiotry tagReposiotry)
        {
            this.TagReposiotry = tagReposiotry;
        }

        public ITagReposiotry TagReposiotry { get; set; }

        [HttpPost(Name = "CreateTag")]
        public ActionResult<TagDto> CreateTag(TagDto tag)
        {
            var tagEntity = new TagEntity()
            {
                Name = tag.Name,
            };

            this.TagReposiotry.Insert(tagEntity);
            tag.Id = tagEntity.Id;

            return this.Ok(tag);
        }

        [HttpGet("{Id}", Name = "GetTagById")]
        public ActionResult<TagDto> GetTagById(int Id)
        {
            return this.Ok(this.TagReposiotry.GetById(Id));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return this.Ok(this.TagReposiotry.GetAll());
        }
    }
}
