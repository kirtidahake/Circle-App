using Circle_App.Data.Models;

namespace Circle_App.Services
{
    public interface IPostService
    {
        Task<List<Posts>> GetAllPostsAsync(int loggedInUserId);
        Task<Posts> CreatePostAsync(Posts post,IFormFile image);
        Task RemovePostAsync(int postId);

        Task AddPostCommentAsync(Comment comment);
        Task RemovePostCommentAsync(int commentId);

        Task TogglePostLiekAsync (int postId, int userId);
        Task TogglePostFavouriteAsync (int postId, int userId);
        Task TogglePostVisibilityAsync (int postId, int userId);
        Task ReportPostAsync(int postId, int userId);
    }
}
