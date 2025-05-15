using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Helpers.Enum;
using Circle_App.Services;
using Circle_App.ViewModels.Story;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesService _storiesService;
        private readonly IFilesService _filesService;

        public StoriesController(IStoriesService storiesService, IFilesService filesService)
        {
            _storiesService = storiesService;
            _filesService = filesService;
        }
       
        public async Task<IActionResult> CreateStory(StoryViewModel model)
        {
            int loggedInUser = 1;
            var imageUploadPath = await _filesService.UploadImageAsync(model.Image, ImageFileType.StoriesImages);
            var newStory = new Story()
            {
                DateCreated = DateTime.UtcNow,
                UserId = loggedInUser,
                ImageUrl = imageUploadPath,
                IsDeleted = false
            };

            await _storiesService.CreateStoryAsync(newStory);             

            return RedirectToAction("Index","Home");
        }
    }
}
