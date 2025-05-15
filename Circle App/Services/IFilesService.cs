using Circle_App.Helpers.Enum;

namespace Circle_App.Services
{
    public interface IFilesService
    {
        Task<string> UploadImageAsync(IFormFile file,ImageFileType imageFileType);
    }
}
