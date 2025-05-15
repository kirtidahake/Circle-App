using Circle_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Circle_App.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly IPostService _postService;
        public FavouritesController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            int loggedInUser = 1;
            var myFavouritePosts = await _postService.GetAllFavouritedPostsAsync(loggedInUser);
            return View(myFavouritePosts);
        }
    }
}
