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
        public ICommentsService CommentsServcie { get; set; }

        public IMapper Mapper { get; set; }

        public CommentController(ICommentsService commentsServcie, IMapper mapper)
        {
            CommentsServcie = commentsServcie;
            Mapper = mapper;
        }

        [HttpGet("{Id}", Name = "GetCommentById")]
        public ActionResult<CommentDto> GetCommentById(int Id)
        {
            var comment = this.CommentsServcie.GetCommentById(Id);
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
            return Ok(CommentsServcie.GetAll());
        }

        [HttpPost(Name = "CreateComment")]
        public ActionResult<CommentDto> CreateComment(CommentDto commentDto)
        {
            var newComment = CommentsServcie.CreateComment(Mapper.Map<Comment>(commentDto));
            return Ok(Mapper.Map<CommentDto>(newComment));
        }

        [HttpDelete("{Id}", Name = "DeleteComment")]
        public IActionResult DeleteComment(int Id)
        {
            CommentsServcie.DeleteComment(Id);

            return Ok();
        }
    }
}
