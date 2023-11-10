using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITagService
    {
        public Tag CreateTag(int todoTaskId, string tag);

        public Tag GetTag(int id);

        public void DeleteTag(string name);

        public IQueryable<Tag> GetAll();

        void DeleteTagFromTodoTask(int todoTaskId, string name);
    }
}
