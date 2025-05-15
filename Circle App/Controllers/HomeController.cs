using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Helpers;
using Circle_App.Helpers.Enum;
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
        private readonly IHashtagService _hashtagService;
        private readonly IPostService _postService;
        private readonly IFilesService _filesService;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IHashtagService hashtagService, IFilesService filesService)
        {
            _logger = logger;
            _hashtagService = hashtagService;
            _postService = postService;
            _filesService = filesService;
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
            var imageUploadPath = await _filesService.UploadImageAsync(post.Image, ImageFileType.PostImages);
            var newPost = new Posts()
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUploaded = DateTime.UtcNow,
                ImageUrl = imageUploadPath,
                NrOfReports = 0,
                UserId = loggedInUser
            };

            await _postService.CreatePostAsync(newPost);
            await _hashtagService.ProcessHashtagForNewPostAsync(post.Content);
            

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
            var postRemoved = await _postService.RemovePostAsync(model.PostId);
            await _hashtagService.ProcessHashtagForRemovePostAsync(postRemoved.Content);
               
            return RedirectToAction("Index");
        }
    }
}
