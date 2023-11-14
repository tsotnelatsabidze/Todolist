using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            _ = this.CreateMap<Services.Models.Comment, Models.Models.CommentDto>().ReverseMap();
        }
    }
}
