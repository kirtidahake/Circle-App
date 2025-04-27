using System.ComponentModel.DataAnnotations;

namespace Circle_App.Data.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public int NrOfReports { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUploaded { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
