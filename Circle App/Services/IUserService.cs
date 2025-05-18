using Circle_App.Data.Models;

namespace Circle_App.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int loggedInUser);
    }
}
