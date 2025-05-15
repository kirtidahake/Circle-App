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

        public async Task<Story> CreateStoryAsync(Story story)
        {
            
            await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
            return story;
        }
    }
}
