using Circle_App.Data;
using Circle_App.Data.Models;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allPosts = await _context.Posts
                .Include(u => u.User)
                .Include(u => u.Likes)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
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

            if (post.Image != null && post.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (post.Image.ContentType.Contains("image"))
                {
                    string rooFolderPathImages = Path.Combine(rootFolderPath, "images/uploaded");
                    Directory.CreateDirectory(rooFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                    string filePath = Path.Combine(rooFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await post.Image.CopyToAsync(stream);

                    newPost.ImageUrl = "/images/uploaded/" + fileName;
                }
            }
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeViewModel postLikeVM)
         {
            int loggedInUserId = 1;

            var like = await _context.Likes
                .Where(l => l.UserId == loggedInUserId && l.PostId == postLikeVM.PostId)
                .FirstOrDefaultAsync();

            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
            else {
                var newLike = new Likes()
                {
                    PostId = postLikeVM.PostId,
                    UserId = loggedInUserId
                };
                _context.Likes.Add(newLike);
                await _context.SaveChangesAsync();
            }
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
            await _context.Comments.AddAsync(newCommnet);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
    }
}
