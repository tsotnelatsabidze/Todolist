using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IMapper mapper;

        public CommentController(ICommentReposiotry commentReposiotry, IMapper mapper)
        {
            this.CommentReposiotry = commentReposiotry;
            this.mapper = mapper;
        }

        public ICommentReposiotry CommentReposiotry { get; set; }

        [HttpPost(Name = "AddComment")]
        public ActionResult<CommentDto> AddComment(CommentDto comment)
        {
            var commentToAdd = this.mapper.Map<CommentEntity>(comment);
            commentToAdd.CreatDate = DateTime.Now;
            this.CommentReposiotry.Insert(commentToAdd);

            return this.Ok(this.mapper.Map<CommentDto>(commentToAdd));
        }

        [HttpGet("{Id}", Name = "GetCommentById")]
        public ActionResult<CommentDto> GetCommentById(int Id)
        {
            return this.Ok(this.CommentReposiotry.GetById(Id));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetComments()
        {
            return this.Ok(this.mapper.ProjectTo<CommentDto>(this.CommentReposiotry.GetAll()));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = this.CommentReposiotry.GetById(id);

            if (comment == null)
            {
                return this.NotFound();
            }

            this.CommentReposiotry.Delete(comment);

            return this.NoContent();
        }
    }
}
