using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TagWebApiService
    {
        public TagWebApiService()
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/"),
            };
        }

        public HttpClient Client { get; set; }

        public async Task<IEnumerable<TagDto>> GetAllTags()
        {
            var response = await this.Client.GetAsync("Tag/");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TagDto>>(content);
        }

        public async Task AddTagToTodoTask(int todoTaskId, string tag)
        {
            _ = await this.Client.PostAsync($"Tag?todoTaskId={todoTaskId}&tag={tag}", null);
        }

        public async Task RemoveTagFromTodoTask(int todoTaskId, string tag)
        {
            _ = await this.Client.DeleteAsync($"Tag?todoTaskId={todoTaskId}&tag={tag}");
        }
    }
}
