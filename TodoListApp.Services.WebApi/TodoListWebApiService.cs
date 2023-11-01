using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoListApp.WebApi.Models.Models;

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
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5276/"),
            };
        }

        public List<TodoList> GetTodoLists()
        {
            var response = this.Client.GetAsync("/TodoList").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoList>>(content);
        }

        public async Task<TodoList> GetTodoListDetails(int id)
        {
            var response = await this.Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=Id eq {id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoList>>(content).First();
        }

        public async Task<IEnumerable<TodoList>> GetTodoListForUser(string userId)
        {
            var response = await this.Client.GetAsync($"TodoList?$expand=TodoTasks&$filter=CreatorUserId eq '{userId}'");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoList>>(content);
        }

        public async Task<TodoList> GetTodoList(int id)
        {
            var response = await this.Client.GetAsync($"/TodoList/{id}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public async Task<TodoList> AddNewAsync(TodoListCreateDto todoList)
        {
            var response = await this.Client.PostAsJsonAsync("/TodoList/", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public async Task<TodoList> Delete(int todoListId)
        {
            var response = await this.Client.DeleteAsync($"/TodoList/{todoListId}");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public async Task<TodoTask> AddNewTaskAsync(TodoTaskCreateDto todoTask)
        {
            var response = await this.Client.PostAsJsonAsync("/TodoTask/", todoTask);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTask>(content);
        }

        public IEnumerable<TodoTask> GetToDoTasksByToDoList(int todoListId)
        {
            var response = this.Client.GetAsync($"/TodoTask?$filter=todoListId eq {todoListId}").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content);
        }

        public async Task<TodoList> Update(int id, TodoList todoList)
        {
            var response = await this.Client.PutAsJsonAsync($"/TodoList/{id}", todoList);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoList>(content);
        }

        public async Task<TodoTask> GetTodoTaskById(int taskId)
        {
            var response = await this.Client.GetAsync($"/TodoTask?$filter=Id eq {taskId}&$expand=Tags");
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoTask>>(content).FirstOrDefault();
        }

        public List<TodoTask> GetTodoTasks()
        {
            var response = this.Client.GetAsync("/TodoTask").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TodoTask>>(content);
        }

        public async Task<TodoTask> UpdateTodoTask(int id, TodoTaskUpdateDto todoTaskUpdateDTO)
        {
            var response = await this.Client.PutAsJsonAsync($"/TodoTask/{id}", todoTaskUpdateDTO);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoTask>(content);
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

        public async Task AddCommentToTodoTask(int todoTaskId, string comment)
        {
            var todoTask = await this.GetTodoTaskById(todoTaskId);
            if (todoTask.Comments.Any(t => t.Name == comment))
            {
                return;
            }
            else
            {
                todoTask.Comment.Add(new CommentDto()
                {
                    Name = comment,
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
                Comments = todoTask.Comments,
            });
        }

        public async Task RemoveCommentFromTodoTask(int todoTaskId, string comment)
        {
            var todoTask = await this.GetTodoTaskById(todoTaskId);
            if (todoTask.Comments.Any(t => t.Name == comment))
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
                    Comments = todoTask.Comments.Where(x => x.Name != comment),
                });
            }
        }
    }
}
