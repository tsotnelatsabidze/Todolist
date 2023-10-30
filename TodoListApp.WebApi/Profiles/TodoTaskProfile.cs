using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoTaskProfile : Profile
    {
        public TodoTaskProfile()
        {
            _ = this.CreateMap<Services.Models.TodoTask, Models.Models.TodoTask>();
            _ = this.CreateMap<Models.Models.TodoTask, Services.Models.TodoTask>();
        }
    }
}
