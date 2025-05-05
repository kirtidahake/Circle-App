using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.ViewModels.Story;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Controllers
{
    public class StoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoriesController(ApplicationDbContext context)
        {
            _context = context;
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

            if (model.Image != null && model.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (model.Image.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, "images/stories");
                    Directory.CreateDirectory(rootFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await model.Image.CopyToAsync(stream);

                    newStory.ImageUrl = "images/stories/" + fileName;
                }
            }
             await  _context.Stories.AddAsync(newStory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }
    }
}
