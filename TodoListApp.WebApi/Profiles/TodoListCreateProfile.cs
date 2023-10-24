using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoListCreateProfile : Profile
    {
        public TodoListCreateProfile()
        {
            _ = this.CreateMap<Services.Models.TodoList, Models.Models.TodoListCreateDTO>();
            _ = this.CreateMap<Models.Models.TodoListCreateDTO, Services.Models.TodoList>();
        }
    }
}
