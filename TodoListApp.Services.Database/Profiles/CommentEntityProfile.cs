using AutoMapper;

namespace TodoListApp.Services.Database.Profiles
{
    public class CommentEntityProfile : Profile
    {
        public CommentEntityProfile()
        {
            _ = this.CreateMap<Entities.CommentEntity, Models.Comment>().ReverseMap();
        }
    }
}
