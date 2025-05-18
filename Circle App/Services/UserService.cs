using Circle_App.Data;
using Circle_App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Circle_App.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(int loggedInUser)
        {
            
            return await _context.Users.FirstOrDefaultAsync(n => n.UserId == loggedInUser) ?? new User();
        }
    }
}
