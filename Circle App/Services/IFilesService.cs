namespace Circle_App.Services
{
    public interface IFilesService
    {
        Task<string> UploadImageAsync(IFormFile file,string fileType);
    }
}
