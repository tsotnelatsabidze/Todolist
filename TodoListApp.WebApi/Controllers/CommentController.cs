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
    public class CommentController : ControllerBase
    {
        public ICommentReposiotry CommentReposiotry { get; set; }

        public CommentController(ICommentReposiotry commentReposiotry)
        {
            this.CommentReposiotry = commentReposiotry;
        }

        [HttpPost(Name = "AddComment")]
        public ActionResult<TagDto> AddComment(CommentDto comment)
        {
            var commentEntity = new CommentEntity()
            {
                Name = comment.Name,
            };

            this.CommentReposiotry.Insert(commentEntity);
            comment.Id = commentEntity.Id;

            return this.Ok(comment);
        }

        [HttpGet("{Id}", Name = "GetCommentById")]
        public ActionResult<TagDto> GetTagById(int Id)
        {
            return this.Ok(this.CommentReposiotry.GetById(Id));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllTodoTasks()
        {
            return this.Ok(this.CommentReposiotry.GetAll());
        }
    }
}
