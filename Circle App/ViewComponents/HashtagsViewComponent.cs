using Microsoft.AspNetCore.Mvc;

namespace Circle_App.ViewComponents
{
    public class HashtagsViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
