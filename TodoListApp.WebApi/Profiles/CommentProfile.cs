using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            _ = this.CreateMap<Models.Models.CommentDto, Services.Models.Comment>();
        }
    }
}
