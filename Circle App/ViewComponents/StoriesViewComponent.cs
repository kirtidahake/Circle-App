
using Circle_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace Circle_App.ViewComponents
{
    public class StoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public StoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allStories = _context.Stories
                .Where(u => u.DateCreated >= DateTime.UtcNow.AddHours(-24))
                .Include(u => u.User)
                .ToList();
            return View(allStories);
        }

    }
}
