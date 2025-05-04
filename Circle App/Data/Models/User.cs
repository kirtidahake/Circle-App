using System.ComponentModel.DataAnnotations;

namespace Circle_App.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Posts> Posts { get; set; } = new List<Posts>();
        public ICollection<Story> Stories { get; set; } = new List<Story>();
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Favourites> Favourites { get; set; } = new List<Favourites>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();


    }
}
