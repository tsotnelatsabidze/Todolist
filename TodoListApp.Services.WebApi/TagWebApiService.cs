using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TagWebApiService
    {
        public HttpClient Client { get; set; }

        public TagWebApiService()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("https://localhost:7052/");
        }

        public async Task<IEnumerable<TagDto>> GetAllTags()
        {
            var response = await Client.GetAsync("Tag/");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TagDto>>(content);
        }

        public async Task AddTagToTodoTask(int todoTaskId, string tag)
        {
            var response = await Client.PostAsync($"Tag?todoTaskId={todoTaskId}&tag={tag}", null);
        }

        public async Task RemoveTagFromTodoTask(int todoTaskId, string tag)
        {
            var response = await Client.DeleteAsync($"Tag?todoTaskId={todoTaskId}&tag={tag}");
        }
    }
}
