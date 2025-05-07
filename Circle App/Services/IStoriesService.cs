using Circle_App.Data.Models;

namespace Circle_App.Services
{
    public interface IStoriesService
    {
        Task<List<Story>> GetAllStoriesAsync();
        Task<Story> CreateStoryAsync(Story story, IFormFile image);
    }
}
