using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        public CommentController(ICommentsService commentsServcie, IMapper mapper)
        {
            this.CommentsServcie = commentsServcie;
            this.Mapper = mapper;
        }

        public ICommentsService CommentsServcie { get; set; }

        public IMapper Mapper { get; set; }

        [HttpGet("{Id}", Name = "GetCommentById")]
        public ActionResult<CommentDto> GetCommentById(int id)
        {
            var comment = this.CommentsServcie.GetCommentById(id);
            if (comment == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.Mapper.Map<CommentDto>(comment));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllComments()
        {
            return this.Ok(this.CommentsServcie.GetAll());
        }

        [HttpPost(Name = "CreateComment")]
        public ActionResult<CommentDto> CreateComment(CommentDto commentDto)
        {
            var newComment = this.CommentsServcie.CreateComment(this.Mapper.Map<Comment>(commentDto));
            return this.Ok(this.Mapper.Map<CommentDto>(newComment));
        }

        [HttpDelete("{Id}", Name = "DeleteComment")]
        public IActionResult DeleteComment(int id)
        {
            this.CommentsServcie.DeleteComment(id);

            return this.Ok();
        }
    }
}
