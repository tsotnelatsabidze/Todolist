using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.Services.Models;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoTasksWebApiService
    {
        public HttpClient Client { get; set; }

        public TodoTasksWebApiService()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("https://localhost:7052/");
        }

        public async Task<IEnumerable<TodoTask>> GetToDoTasksByTag(int tagId)
        {
            var response = Client.GetAsync($"TodoTask?$filter=Tags/any(tag: tag/Id eq {tagId})").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content);
        }

        public async Task<TodoTask> AddNewTaskAsync(TodoTaskCreateDto todoTask)
        {
            var response = await Client.PostAsJsonAsync("/TodoTask/", todoTask);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public IEnumerable<TodoTask> GetToDoTasksByToDoList(int todoListId)
        {
            var response = Client.GetAsync($"/TodoTask?$filter=todoListId eq {todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content);
        }

        public async Task<TodoTask> GetTodoTaskById(int taskId)
        {
            var response = await Client.GetAsync($"/TodoTask?$filter=Id eq {taskId}&$expand=Tags,Comments");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content).FirstOrDefault();
        }

        public List<TodoTask> GetTodoTasks()
        {
            var response = Client.GetAsync("/TodoTask").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoTask>>(content);
        }

        public async Task<TodoTask> UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskUpdateDTO)
        {
            var response = await Client.PutAsJsonAsync($"/TodoTask/{id}", todoTaskUpdateDTO);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public async Task DeleteTodoTask(int id)
        {
            _ = await Client.DeleteAsync($"/TodoTask/{id}");
        }
    }
}
