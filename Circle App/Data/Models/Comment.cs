namespace Circle_App.Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public Posts Post { get; set; }
        public User User { get; set; }
    }
}
