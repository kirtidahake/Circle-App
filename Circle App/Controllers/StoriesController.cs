using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Services;
using Circle_App.ViewModels.Story;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }
       
        public async Task<IActionResult> CreateStory(StoryViewModel model)
        {
            int loggedInUser = 1;

            var newStory = new Story()
            {
                DateCreated = DateTime.UtcNow,
                UserId = loggedInUser,
                IsDeleted = false
            };

            await _storiesService.CreateStoryAsync(newStory, model.Image);             

            return RedirectToAction("Index","Home");
        }
    }
}
