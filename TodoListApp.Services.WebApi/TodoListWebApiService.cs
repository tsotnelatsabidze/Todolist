using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TodoListApp.Services.WebApi
{
    public class TodoListWebApiService
    {
        private readonly HttpClient httpClient;

        public TodoListWebApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TodoList> GetTodoListAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"api/todolist/{id}");
            _ = response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TodoList>();
        }

        public async Task<List<TodoList>> GetTodoListsAsync()
        {
            var response = await this.httpClient.GetAsync("api/todolist");
            _ = response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TodoList>>();
        }

        public async Task<bool> CreateTodoListAsync(TodoList todoList)
        {
            var content = new StringContent(JsonSerializer.Serialize(todoList), Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/todolist", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTodoListAsync(TodoList todoList)
        {
            var content = new StringContent(JsonSerializer.Serialize(todoList), Encoding.UTF8, "application/json");
            var response = await this.httpClient.PutAsync($"api/todolist/{todoList.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTodoListAsync(int id)
        {
            var response = await this.httpClient.DeleteAsync($"api/todolist/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
