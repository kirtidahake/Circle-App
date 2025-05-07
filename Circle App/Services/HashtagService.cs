
using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Services
{
    public class HashtagService : IHashtagService
    {
        private readonly ApplicationDbContext _context;
        public HashtagService( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task ProcessHashtagForNewPostAsync(string content)
        {
            //Find and store Hashtags
            var postHashtags = HashtagHelpers.GetHashtage(content);
            foreach (var items in postHashtags)
            {
                var hashtagExists = await _context.Hashtag.FirstOrDefaultAsync(h => h.HashtagName == items);
                if (hashtagExists != null)
                {
                    hashtagExists.Count += 1;
                    hashtagExists.DateUpdated = DateTime.UtcNow;

                    _context.Hashtag.Update(hashtagExists);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newHashtag = new Hashtag()
                    {
                        HashtagName = items,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };
                    _context.Hashtag.Add(newHashtag);
                    await _context.SaveChangesAsync();
                }
            };
        }

        public async Task ProcessHashtagForRemovePostAsync(string content)
        {
            //Update Hashtag
            var postHashtags = HashtagHelpers.GetHashtage(content);
            foreach (var items in postHashtags)
            {
                var hashtagExists = await _context.Hashtag.FirstOrDefaultAsync(h => h.HashtagName == items);
                if (hashtagExists != null)
                {
                    hashtagExists.Count -= 1;
                    hashtagExists.DateUpdated = DateTime.UtcNow;

                    _context.Hashtag.Update(hashtagExists);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
