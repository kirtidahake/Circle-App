using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Migrations;

namespace Circle_App.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext AppDbContext)
        {
            if (!AppDbContext.Users.Any() && !AppDbContext.Posts.Any())
            {
                var newUser = new User()
                {
                    UserName = "Kirti Dahake",
                    ProfilePictureUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vecteezy.com%2Fvector-art%2F14212681-female-user-profile-avatar-is-a-woman-a-character-for-a-screen-saver-with-emotions-for-website-and-mobile-app-design-vector-illustration-on-a-white-isolated-background&psig=AOvVaw1ES8H_juSKI0JfPtR3bEDR&ust=1745844637138000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCNjozp-g-IwDFQAAAAAdAAAAABAE"
                };
                await AppDbContext.Users.AddAsync(newUser);
                await AppDbContext.SaveChangesAsync();

                var newPost = new Posts()
                {
                    Content = "This is a new post",
                    ImageUrl = "https://png.pngtree.com/background/20211215/original/pngtree-sunset-beach-cartoon-style-pink-background-picture-image_1467289.jpg",
                    NrOfReports = 1,
                    DateCreated = DateTime.UtcNow,
                    DateUploaded = DateTime.UtcNow,

                    UserId = newUser.UserId
                };
                await AppDbContext.Posts.AddAsync(newPost);
                await AppDbContext.SaveChangesAsync();

            }

        }
    }
}
