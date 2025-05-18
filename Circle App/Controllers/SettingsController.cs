using Circle_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Circle_App.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUserService _userService;
        public SettingsController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUser = 1;
            var userDb = await _userService.GetUser(loggedInUser);
            return View(userDb);
        }
    }
}
