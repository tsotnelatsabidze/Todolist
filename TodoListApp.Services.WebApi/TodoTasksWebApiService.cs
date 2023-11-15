using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoTasksWebApiService
    {
        public TodoTasksWebApiService(string baseUrl)
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
            };
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
            var todoTask = JsonConvert.DeserializeObject<IEnumerable<TodoTaskDto>>(content).FirstOrDefault();

            return todoTask ?? throw new KeyNotFoundException($"TodoTask with id {taskId} not found");
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
