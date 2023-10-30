using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoListWebApiService
    {
        public TodoListWebApiService()
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/"),
            };
        }

        public HttpClient Client { get; set; }

        public List<TodoList> GetTodoLists()
        {
            var response = this.Client.GetAsync("/TodoList").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoList>>(content);
        }

        public TodoList GetTodoListDetails(int id)
        {
            var response = this.Client.GetAsync($"/TodoList/{id}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public TodoList AddNew(TodoListCreateDto todoList)
        {
            var response = this.Client.PostAsJsonAsync("/TodoList/", todoList).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public void DeleteTodoList(int todoListId)
        {
            var response = this.Client.DeleteAsync($"/TodoList/{todoListId}").Result;
            _ = response.Content.ReadAsStringAsync().Result;
        }

        public TodoTask AddNewTask(TodoTaskCreateDto todoTask)
        {
            var response = this.Client.PostAsJsonAsync("/TodoTask/", todoTask).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public IEnumerable<TodoTask> GetToDoTasksByToDoList(int todoListId)
        {
            var response = this.Client.GetAsync($"/TodoTask?$filter=todoListId eq {todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content);
        }

        public TodoList UpdateToDoList(int id, TodoListUpdateDto todoListUpdateDTO)
        {
            var response = this.Client.PutAsJsonAsync($"/TodoList/{id}", todoListUpdateDTO).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public TodoTask GetTodoTaskById(int taskId)
        {
            var response = this.Client.GetAsync($"/TodoTask/{taskId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public TodoTask UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskUpdateDTO)
        {
            var response = this.Client.PutAsJsonAsync($"/TodoTask/{id}", todoTaskUpdateDTO).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public void DeleteTodoTask(int id)
        {
            _ = this.Client.DeleteAsync($"/TodoTask/{id}").Result;
        }
    }
}
