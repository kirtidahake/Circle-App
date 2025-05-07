using Circle_App.Data;
using Circle_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly ApplicationDbContext _context;

        public StoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Story>> GetAllStoriesAsync()
        {
            var allStories = await _context.Stories
               //.Where(u => u.DateCreated >= DateTime.UtcNow.AddHours(-24))
               .Include(u => u.User)
               .ToListAsync();

            return allStories;
        }

        public async Task<Story> CreateStoryAsync(Story story, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (image.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, "images/stories");
                    Directory.CreateDirectory(rootFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await image.CopyToAsync(stream);

                    story.ImageUrl = "images/stories/" + fileName;
                }
            }
            await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
            return story;
        }
    }
}
