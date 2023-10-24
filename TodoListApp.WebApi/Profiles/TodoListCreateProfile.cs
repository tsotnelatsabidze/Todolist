using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoListCreateProfile : Profile
    {
        public TodoListCreateProfile()
        {
            _ = this.CreateMap<Services.Models.TodoList, Models.Models.TodoListCreateDto>();
            _ = this.CreateMap<Models.Models.TodoListCreateDto, Services.Models.TodoList>();
        }
    }
}
