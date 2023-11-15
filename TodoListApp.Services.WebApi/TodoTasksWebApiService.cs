using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoTasksWebApiService
    {
        public TodoTasksWebApiService()
        {
#pragma warning disable S1075 // URIs should not be hardcoded
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/"),
            };
#pragma warning restore S1075 // URIs should not be hardcoded
        }

        public HttpClient Client { get; set; }

        public IEnumerable<TodoTaskDto> GetToDoTasksByTag(int tagId)
        {
            var response = this.Client.GetAsync($"TodoTask?$filter=Tags/any(tag: tag/Id eq {tagId})").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTaskDto>>(content);
        }

        public async Task<TodoTaskDto> AddNewTaskAsync(TodoTaskCreateDto todoTask)
        {
            var response = await this.Client.PostAsJsonAsync("/TodoTask/", todoTask);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTaskDto>(content);
        }

        public IEnumerable<TodoTaskDto> GetToDoTasksByToDoList(int todoListId)
        {
            var response = this.Client.GetAsync($"/TodoTask?$filter=todoListId eq {todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTaskDto>>(content);
        }

        public async Task<TodoTaskDto> GetTodoTaskById(int taskId)
        {
            var response = await this.Client.GetAsync($"/TodoTask?$filter=Id eq {taskId}&$expand=Tags,Comments");
            string content = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<IEnumerable<TodoTaskDto>>(content).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<TodoTaskDto> GetTodoTasks()
        {
            var response = this.Client.GetAsync("/TodoTask").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoTaskDto>>(content);
        }

        public async Task<TodoTaskDto> UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskUpdateDTO)
        {
            var response = await this.Client.PutAsJsonAsync($"/TodoTask/{id}", todoTaskUpdateDTO);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTaskDto>(content);
        }

        public async Task DeleteTodoTask(int id)
        {
            _ = await this.Client.DeleteAsync($"/TodoTask/{id}");
        }
    }
}
