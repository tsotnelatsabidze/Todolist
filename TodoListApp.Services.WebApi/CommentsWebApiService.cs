using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class CommentsWebApiService
    {
        public CommentsWebApiService(string baseUrl)
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
            };
        }

        public HttpClient Client { get; set; }

        public async Task<List<CommentDto>> GetCommentsByTodoTaskId()
        {
            var response = await this.Client.GetAsync("/Comment");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CommentDto>>(content);
        }

        public async Task<CommentDto> CreateNewComment(CommentDto comment)
        {
            var response = await this.Client.PostAsJsonAsync("/Comment", comment);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommentDto>(content);
        }

        public async Task DeleteComment(int commentId)
        {
            _ = await this.Client.DeleteAsync($"/Comment/{commentId}");
        }
    }
}
