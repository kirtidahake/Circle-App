using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Helpers;
using Circle_App.Services;
using Circle_App.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Circle_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IPostService postService)
        {
            _logger = logger;
            _context = context;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            int loggedInUser = 1;
            var allPosts = await _postService.GetAllPostsAsync(loggedInUser);
            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel post)
        {
            int loggedInUser = 1;

            var newPost = new Posts()
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUploaded = DateTime.UtcNow,
                ImageUrl = "",
                NrOfReports = 0,
                UserId = loggedInUser
            };

            await _postService.CreatePostAsync(newPost, post.Image);
            //Find and store Hashtags
            var postHashtags = HashtagHelpers.GetHashtage(post.Content);
            foreach (var items in postHashtags)
            {
                var hashtagExists = await _context.Hashtag.FirstOrDefaultAsync(h => h.HashtagName == items);
                if (hashtagExists != null)
                {
                    hashtagExists.Count += 1;
                    hashtagExists.DateUpdated = DateTime.UtcNow;

                    _context.Hashtag.Update(hashtagExists);
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    var newHashtag = new Hashtag()
                    {
                        HashtagName = items,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };
                    _context.Hashtag.Add(newHashtag);
                    await _context.SaveChangesAsync();
                }
            };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeViewModel postLikeVM)
         {
            int loggedInUserId = 1;
            await _postService.TogglePostLiekAsync(postLikeVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentViewModel model)
        {
            int loggedInUserId = 1;

            var newCommnet = new Comment()
            {
                UserId = loggedInUserId,
                PostId = model.PostId,
                CommentContent = model.CommentContent,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _postService.AddPostCommentAsync(newCommnet);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemovePostCommentViewModel model)
        {
            await _postService.RemovePostCommentAsync(model.CommentId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostFavourite(FavouriteViewModel model)
        {
            int loggedInUserId = 1;
            await _postService.TogglePostFavouriteAsync(model.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostVisibility(PostVisibilityViewModel model)
        {
            int loggedInUserId = 1;
            await _postService.TogglePostVisibilityAsync(model.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostReport(PostReportViewModel model)
        {
            int loggedInUserId = 1;
            await _postService.ReportPostAsync(model.PostId, loggedInUserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PostDelete(RemovePostViewModel model)
        {
            await _postService.RemovePostAsync(model.PostId);
               //Update Hashtag
                //var postHashtags = HashtagHelpers.GetHashtage(postExists.Content);
                //foreach (var items in postHashtags)
                //{
                //    var hashtagExists = await _context.Hashtag.FirstOrDefaultAsync(h => h.HashtagName == items);
                //    if (hashtagExists != null)
                //    {
                //        hashtagExists.Count -= 1;
                //        hashtagExists.DateUpdated = DateTime.UtcNow;

                //        _context.Hashtag.Update(hashtagExists);
                //        await _context.SaveChangesAsync();
                //    }
                //}
            return RedirectToAction("Index");
        }
    }
}
