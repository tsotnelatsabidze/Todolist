using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoTaskUpdateProfile : Profile
    {
        public TodoTaskUpdateProfile()
        {
            _ = this.CreateMap<Models.Models.TodoTaskUpdateDto, Services.Models.TodoTask>();
        }
    }
}
