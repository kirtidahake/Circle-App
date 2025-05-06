namespace Circle_App.Data.Models
{
    public class Hashtag
    {
        public int HashtagId { get; set; }
        public string HashtagName { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
