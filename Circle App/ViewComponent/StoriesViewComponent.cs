
using Circle_App.Data;
using Circle_App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Circle_App.ViewComponents
{
    public class StoriesViewComponent : ViewComponent
    {
        private readonly IStoriesService _storiesService;

        public StoriesViewComponent(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allStories = await _storiesService.GetAllStoriesAsync();
            return View(allStories);
        }

    }
}
