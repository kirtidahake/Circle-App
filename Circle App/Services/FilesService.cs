
using Circle_App.Data;
using Circle_App.Data.Models;
using Circle_App.Helpers.Enum;
using static System.Net.Mime.MediaTypeNames;

namespace Circle_App.Services
{
    public class FilesService : IFilesService
    {
        private readonly ApplicationDbContext _context;
        public FilesService(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<string> UploadImageAsync(IFormFile file, ImageFileType imageFileType)
        {
            var filePathUpload = imageFileType switch
            {
                ImageFileType.PostImages => Path.Combine("images","posts"),
                ImageFileType.StoriesImages => Path.Combine("images", "stories"),
                ImageFileType.ProficePictures => Path.Combine("images", "porfilepics"),
                ImageFileType.CoverImages => Path.Combine("images", "covers"),
                _ => throw new ArgumentException("Invalid File Type")
            }; 
            if (file != null && file.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (file.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, filePathUpload);
                    Directory.CreateDirectory(rootFolderPathImages);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    return $"{filePathUpload}/{fileName}" ;
                }
            }
            return ""; 
        }
    }
}
