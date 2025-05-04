namespace Circle_App.Data.Models
{
    public class Story
    {
        public int StoryId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

       
    }
}
