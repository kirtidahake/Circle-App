
using Circle_App.Data;

namespace Circle_App.Services
{
    public class FilesService : IFilesService
    {
        private readonly ApplicationDbContext _context;
        public FilesService(ApplicationDbContext context)
        {
            _context = context; 
        }
        public Task<string> UploadImageAsync(IFormFile file, string fileType)
        {
            throw new NotImplementedException();
        }
    }
}
