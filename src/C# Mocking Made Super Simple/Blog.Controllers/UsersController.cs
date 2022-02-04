namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Images;
    using Services.Machine;
    using static ControllerConstants;

    public class UsersController : Controller
    {
        private readonly IImageService imageService;
        private readonly IFileSystemService fileSystemService;

        public UsersController(
            IImageService imageService,
            IFileSystemService fileSystemService)
        {
            this.imageService = imageService;
            this.fileSystemService = fileSystemService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userImageDestination = string.Format(
                $"{UserImageDestination}_optimized.jpg", 
                this.User.Identity.Name);

            await using var file = this.fileSystemService
                .OpenRead(userImageDestination);

            return this.File(file, ImageContentType);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(string pictureUrl)
        {
            if (pictureUrl == null)
            {
                return this.BadRequest("Image cannot be empty.");
            }

            var userImageDestination = string.Format(UserImageDestination, this.User.Identity.Name);

            await this.imageService.UpdateImage(pictureUrl, userImageDestination);

            return this.Ok();
        }
    }
}
