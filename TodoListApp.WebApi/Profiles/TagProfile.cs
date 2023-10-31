using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            _ = this.CreateMap<Models.Models.TagDto, Services.Models.Tag>();
        }
    }
}
