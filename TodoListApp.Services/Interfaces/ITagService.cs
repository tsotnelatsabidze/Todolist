using TodoListApp.Services.Models;

namespace TodoListApp.Services.Interfaces
{
    public interface ITagService
    {
        public Tag CreateTag(string tag);

        public Tag GetTag(int id);
    }
}
