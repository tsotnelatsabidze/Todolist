using AutoMapper;
using TodoListApp.Services.Database.Entities;

namespace TodoListApp.WebApi.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            _ = this.CreateMap<Models.Models.CommentDto, CommentEntity>().ReverseMap();
        }
    }
}
