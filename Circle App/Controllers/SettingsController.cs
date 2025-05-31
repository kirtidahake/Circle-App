using Circle_App.Helpers.Enum;
using Circle_App.Services;
using Circle_App.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Circle_App.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFilesService _fileService;
        public SettingsController(IUserService userService, IFilesService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUser = 1;
            var userDb = await _userService.GetUser(loggedInUser);
            return View(userDb);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(UpdateProfilePictureViewModel model)
        {
            var loggedInUser = 1;
            var uploadedProfilePictureImageUrl = await _fileService.UploadImageAsync(model.ProfilePictureImage, ImageFileType.ProficePictures);

            await _userService.UpdateUserProfilePicture(loggedInUser, uploadedProfilePictureImageUrl);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            return RedirectToAction("Index");
        }
    }
}
