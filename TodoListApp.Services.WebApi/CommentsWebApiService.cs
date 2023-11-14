using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class CommentsWebApiService
    {
        public HttpClient Client { get; set; }

        public CommentsWebApiService()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("http://localhost:5276/");
        }

        public async Task<List<CommentDto>> GetCommentsByTodoTaskId()
        {
            var response = await Client.GetAsync("/Comment");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CommentDto>>(content);
        }

        public async Task<CommentDto> CreateNewComment(CommentDto comment)
        {
            var response = await Client.PostAsJsonAsync("/Comment", comment);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CommentDto>(content);
        }

        public async Task DeleteComment(int commentId)
        {
            await this.Client.DeleteAsync($"/Comment/{commentId}");
        }
    }
}
