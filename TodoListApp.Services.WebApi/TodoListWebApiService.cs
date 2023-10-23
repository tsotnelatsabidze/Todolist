namespace TodoListApp.Services.WebApi
{

    public class TodoListWebApiService
    {
        public HttpClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListWebApiService"/> class.
        /// </summary>
        public TodoListWebApiService()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri("https://localhost:7052/");
        }

        public List<TodoList> GetTodoLists()
        {
            var response = Client.GetAsync("/TodoList").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoList>>(content);
        }

        public TodoList GetTodoListDetails(int id)
        {
            var response = Client.GetAsync($"/TodoList/{id}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public TodoList AddNew(TodoListCreateDTO todoList)
        {
            var response = Client.PostAsJsonAsync("/TodoList/", todoList).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public void DeleteTodoList(int todoListId)
        {
            var response = Client.DeleteAsync($"/TodoList/{todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
        }


        public TodoTask AddNewTask(TodoTaskCreateDTO todoTask)
        {
            var response = Client.PostAsJsonAsync("/TodoTask/", todoTask).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public IEnumerable<TodoTask> GetToDoTasksByToDoList(int todoListId)
        {
            var response = Client.GetAsync($"/TodoTask?$filter=todoListId eq {todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content);
        }

        public TodoList UpdateToDoList(int id, TodoListUpdateDTO todoListUpdateDTO)
        {
            var response = Client.PutAsJsonAsync($"/TodoList/{id}", todoListUpdateDTO).Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TodoList>(content);
        }
    }
}
