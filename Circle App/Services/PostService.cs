using Circle_App.Data;
using Circle_App.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Posts>> GetAllPostsAsync(int loggedInUserId)
        {
            var allPosts = await _context.Posts
                .Where(u => (!u.IsPrivate || u.UserId == loggedInUserId) && u.Reports.Count < 5 && !u.IsDeleted)
                .Include(u => u.User)
                .Include(u => u.Likes)
                .Include(u => u.Favourites)
                .Include(u => u.Reports)
                .Include(u => u.Comments).ThenInclude(u => u.User)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
            return allPosts;
        }
        [HttpGet]
        public async Task<List<Posts>> GetAllFavouritedPostsAsync(int loggedInUserId)
        {
            var allFavouritedPosts = await _context.Favourites
                .Include(u => u.Post.Reports)
                .Include(u => u.Post.User)
                .Include (u => u.Post.Likes)
                .Include (u => u.Post.Comments)
                    .ThenInclude(c => c.User)
                .Include(u => u.Post.Favourites)
                .Where(u => u.UserId == loggedInUserId
                    && !u.Post.IsDeleted 
                    && u.Post.Reports.Count < 5)
                .OrderByDescending(u => u.DateCreated)
                .Select(u => u.Post)
                .ToListAsync();
            return allFavouritedPosts;
        }

        public async Task<Posts> CreatePostAsync(Posts post)
        {
            
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }
        
        public async Task<Posts> RemovePostAsync(int postId)
        {
           var postDb = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

            if (postDb != null)
            { 
                postDb.IsDeleted = true;
                _context.Posts.Update(postDb);
                await _context.SaveChangesAsync();
            }
            return postDb;
        }
        public async Task AddPostCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePostCommentAsync(int commentId)
        {
            var commentDb = _context.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (commentDb != null)
            {
                _context.Comments.Remove(commentDb);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ReportPostAsync(int postId, int userId)
        {
            var newReport = new Report()
            {
                PostId = postId,
                UserId = userId,
                DateCreated = DateTime.UtcNow
            };
            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();
        }
        public async Task TogglePostFavouriteAsync(int postId, int userId)
        {
            var favourite = await _context.Favourites
                 .Where(l => l.UserId == userId && l.PostId == postId)
                 .FirstOrDefaultAsync();

            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newFavourite = new Favourites()
                {
                    PostId = postId,
                    UserId = userId,
                    DateCreated = DateTime.UtcNow
                };
                _context.Favourites.Add(newFavourite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task TogglePostLiekAsync(int postId, int userId)
        {
            var like = await _context.Likes
                 .Where(l => l.UserId == userId && l.PostId == postId)
                 .FirstOrDefaultAsync();

            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newLike = new Likes()
                {
                    PostId = postId,
                    UserId = userId
                };
                _context.Likes.Add(newLike);
                await _context.SaveChangesAsync();
            }
        }

        public async Task TogglePostVisibilityAsync(int postId, int userId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
