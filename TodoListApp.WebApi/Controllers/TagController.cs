using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Database.Repositories;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        public ITagReposiotry TagReposiotry { get; set; }

        public TagController(ITagReposiotry tagReposiotry)
        {
            TagReposiotry = tagReposiotry;
        }

        [HttpPost(Name = "CreateTag")]
        public ActionResult<TagDto> CreateTag(TagDto tag)
        {
            var tagEntity = new TagEntity()
            {
                Name = tag.Name
            };

            TagReposiotry.Insert(tagEntity);
            tag.Id = tagEntity.Id;

            return Ok(tag);
        }

        [HttpGet("{Id}", Name = "GetTagById")]
        public ActionResult<TagDto> GetTagById(int Id)
        {
            return Ok(TagReposiotry.GetById(Id));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return Ok(TagReposiotry.GetAll());
        }
    }
}
