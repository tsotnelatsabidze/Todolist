using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{

    public class TodoTaskCreateProfile : Profile
    {
        public TodoTaskCreateProfile()
        {
            _ = this.CreateMap<Models.Models.TodoTaskCreateDto, Services.Models.TodoTask>();
        }
    }
}
