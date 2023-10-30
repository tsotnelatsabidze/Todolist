using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoListUpdateProfile : Profile
    {
        public TodoListUpdateProfile()
        {
            _ = this.CreateMap<Services.Models.TodoList, Models.Models.TodoListUpdateDto>();
            _ = this.CreateMap<Models.Models.TodoListUpdateDto, Services.Models.TodoList>();
        }
    }
}
