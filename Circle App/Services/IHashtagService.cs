namespace Circle_App.Services
{
    public interface IHashtagService
    {
        Task ProcessHashtagForNewPostAsync (string content);
        Task ProcessHashtagForRemovePostAsync (string content);
    }
}
