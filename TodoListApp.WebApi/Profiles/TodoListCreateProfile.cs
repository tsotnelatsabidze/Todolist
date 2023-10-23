using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoListCreateProfile : Profile
    {
        public TodoListCreateProfile()
        {
            CreateMap<Services.Models.TodoList, Models.Models.TodoListCreateDTO>();
            CreateMap<Models.Models.TodoListCreateDTO, Services.Models.TodoList>();
        }
    }
}
