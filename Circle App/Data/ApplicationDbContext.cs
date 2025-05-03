using Circle_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) 
        {
            
        }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Report> Reports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Likes>()
                .HasKey(l => new { l.PostId, l.UserId });

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(l => l.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favourites>()
               .HasKey(l => new { l.PostId, l.UserId });

            modelBuilder.Entity<Favourites>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Favourites)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favourites>()
                .HasOne(l => l.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
              .HasKey(l => new { l.PostId, l.UserId });

            modelBuilder.Entity<Report>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Report>()
                .HasOne(l => l.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
    }
}
