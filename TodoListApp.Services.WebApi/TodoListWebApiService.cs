using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.Services.Models;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.Services.WebApi
{
    public class TodoListWebApiService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListWebApiService"/> class.
        /// </summary>
        public TodoListWebApiService()
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/"),
            };
        }

        public HttpClient Client { get; set; }

        public List<TodoListDto> GetTodoLists()
        {
            var response = this.Client.GetAsync("/TodoList").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoListDto>>(content);
        }

        public async Task<TodoListDto> GetTodoListDetails(int id)
        {
            var response = await this.Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=Id eq {id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoListDto>>(content).First();
        }

        public async Task<IEnumerable<TodoListDto>> GetTodoListForUser(string userId)
        {
            var response = await this.Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=CreatorUserId eq '{userId}'");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoListDto>>(content);
        }

        public async Task<TodoListDto> GetTodoList(int id)
        {
            var response = await this.Client.GetAsync($"/TodoList/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoListDto> AddNewAsync(TodoListCreateDto todoList)
        {
            var response = await this.Client.PostAsJsonAsync("/TodoList/", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoListDto> Delete(int todoListId)
        {
            var response = await this.Client.DeleteAsync($"/TodoList/{todoListId}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
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

        public async Task<TodoListDto> Update(int id, TodoListDto todoList)
        {
            var response = await this.Client.PutAsJsonAsync($"/TodoList/{id}", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoListDto>(content);
        }

        public async Task<TodoTaskDto> GetTodoTaskById(int taskId)
        {
            var response = await this.Client.GetAsync($"/TodoTask?$filter=Id eq {taskId}&$expand=Tags");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoTaskDto>>(content).FirstOrDefault();
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

        public async Task AddTagToTodoTask(int todoTaskId, string tag)
        {
            var todoTask = await this.GetTodoTaskById(todoTaskId);
            if (todoTask.Tags.Any(t => t.Name == tag))
            {
                return;
            }
            else
            {
                todoTask.Tags.Add(new TagDto()
                {
                    Name = tag,
                });
            }

            _ = await this.UpdateTodoTask(todoTaskId, new TodoTaskUpdateDto()
            {
                AssignedUserId = todoTask.AssignedUserId,
                Description = todoTask.Description,
                DueDate = todoTask.DueDate,
                Status = todoTask.Status,
                Title = todoTask.Title,
                TodoListId = todoTask.TodoListId,
                Tags = todoTask.Tags,
            });
        }

        public async Task RemoveTagFromTodoTask(int todoTaskId, string tag)
        {
            var todoTask = await this.GetTodoTaskById(todoTaskId);
            if (todoTask.Tags.Any(t => t.Name == tag))
            {
                _ = await this.UpdateTodoTask(todoTaskId, new TodoTaskUpdateDto()
                {
                    AssignedUserId = todoTask.AssignedUserId,
                    Description = todoTask.Description,
                    DueDate = todoTask.DueDate,
                    Status = todoTask.Status,
                    Title = todoTask.Title,
                    TodoListId = todoTask.TodoListId,
                    Tags = todoTask.Tags.Where(x => x.Name != tag),
                });
            }
        }

        public async Task AddCommentToTodoTask(Comment comment)
        {
            _ = await this.Client.PostAsJsonAsync($"/Comment", comment);
        }

        public async Task DeleteCommentFromTodoTask(Comment comment)
        {
            if (comment is null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            _ = await this.Client.DeleteAsync($"/Comment");
        }
    }
}
