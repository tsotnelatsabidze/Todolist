using AutoMapper;

namespace TodoListApp.Services.Database.Profiles
{
    public class TagEntityProfile : Profile
    {
        public TagEntityProfile()
        {
            _ = this.CreateMap<Entities.TagEntity, Models.Tag>().ReverseMap();
        }
    }
}
