using Circle_App.Data;
using Circle_App.Data.Models;

namespace Circle_App.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        Task IPostService.AddPostCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        Task<List<Posts>> IPostService.CreatePostAsync(Posts post, IFormFile Image)
        {
            throw new NotImplementedException();
        }

        Task<List<Posts>> IPostService.GetAllPostsAsync(int loggedInUserId)
        {
            throw new NotImplementedException();
        }

        Task IPostService.RemovePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        Task IPostService.RemovePostCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        Task IPostService.TogglePostFavouriteAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }

        Task IPostService.TogglePostLiekAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }

        Task IPostService.TogglePostVisibilityAsync(int postId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
