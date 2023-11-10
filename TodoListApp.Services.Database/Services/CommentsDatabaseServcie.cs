using AutoMapper;
using AutoMapper.QueryableExtensions;
using TodoListApp.Services.Database.Entities;
using TodoListApp.Services.Database.Interfaces;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.Database.Services
{
    public class CommentsDatabaseServcie : ICommentsService
    {
        private ICommentRepository CommentRepository { get; set; }

        private IMapper Mapper { get; set; }

        public CommentsDatabaseServcie(ICommentRepository commentRepository, IMapper mapper)
        {
            this.CommentRepository = commentRepository;
            this.Mapper = mapper;
        }


        /// <inheritdoc/>
        public Comment CreateComment(Comment comment)
        {
            var commentEntity = this.Mapper.Map<CommentEntity>(comment);
            this.CommentRepository.Insert(commentEntity);
            return this.Mapper.Map<Comment>(commentEntity);
        }

        /// <inheritdoc/>
        public void DeleteComment(int commentId)
        {
            var commentEntity = this.CommentRepository.GetById(commentId);
            this.CommentRepository.Delete(commentEntity);
        }

        public Comment GetCommentById(int commentId)
        {
            return this.Mapper.Map<Comment>(this.CommentRepository.GetById(commentId));
        }

        public IQueryable<Comment> GetCommentsByTaskId(int taskId)
        {
            var comments = this.CommentRepository.GetAll().Where(x => x.TodoTaskId == taskId).ProjectTo<Comment>(this.Mapper.ConfigurationProvider);
            return comments;
        }

        public IQueryable<Comment> GetAll()
        {
            return this.CommentRepository.GetAll().ProjectTo<Comment>(this.Mapper.ConfigurationProvider);
        }
    }
}
