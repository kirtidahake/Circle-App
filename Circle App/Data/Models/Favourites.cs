namespace Circle_App.Data.Models
{
    public class Favourites
    {
        public int FavouritesId { get; set; }
        public DateTime DateCreated { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public Posts Post { get; set; }
        public User User { get; set; }
    }
}
