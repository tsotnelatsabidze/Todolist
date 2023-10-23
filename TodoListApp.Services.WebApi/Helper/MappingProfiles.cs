using AutoMapper;
using TodoListApp.Services.Models;

namespace TodoListApp.Services.WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            _ = this.CreateMap<TodoTask, TodoTask>();
            _ = this.CreateMap<TodoList, TodoList>();
            _ = this.CreateMap<TodoList, TodoList>();
            _ = this.CreateMap<TodoTask, TodoTask>();
        }
    }
}
