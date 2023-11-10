using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.WebApi;

namespace TodoListApp.WebApp.Controllers
{
    public class TagController : Controller
    {
        public TagWebApiService TagWebApiService { get; set; }

        public TagController(TagWebApiService tagWebApiService)
        {
            this.TagWebApiService = tagWebApiService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.TagWebApiService.GetAllTags());
        }
    }
}
