using AutoMapper;

namespace TodoListApp.WebApi.Profiles
{
    public class TodoTaskProfile : Profile
    {
        public TodoTaskProfile()
        {
            _ = this.CreateMap<Services.Models.TodoTask, Models.Models.TodoTaskDto>();
            _ = this.CreateMap<Models.Models.TodoTaskDto, Services.Models.TodoTask>();
        }
    }
}
