using Microsoft.AspNetCore.Mvc;

namespace Circle_App.Controllers
{
    public class Settings : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
