namespace Circle_App.Data.Models
{
    public class Likes
    {
        public int LikesId { get; set; }
        public int PostId   { get; set; }
        public int UserId { get; set; }
        public Posts Posts { get; set; }
        public User User { get; set; }
    }
}
