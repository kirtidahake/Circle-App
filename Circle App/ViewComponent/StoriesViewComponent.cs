
using Circle_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var allStories = _context.Stories.Include(u => u.User).ToList();
            return View(allStories);
        }

    }
}
