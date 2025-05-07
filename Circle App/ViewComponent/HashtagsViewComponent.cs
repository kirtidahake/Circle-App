using Circle_App.Data;
using Microsoft.AspNetCore.Mvc;

namespace Circle_App.ViewComponents
{
    public class HashtagsViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public HashtagsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

            var topThreeHashtags = _context.Hashtag
                .Where(h => h.DateCreated >= oneWeekAgo)
                .OrderByDescending(h => h.Count)
                .Take(3)
                .ToList();
            return View(topThreeHashtags);
        }
    }
}
