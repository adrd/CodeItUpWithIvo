namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    using FileSystem = System.IO.File;

    public class UsersController : Controller
    {
        private const string UserImageDestination = @"Images\Users\{0}";
        private const string ImageContentType = "image/jpeg";

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userImageDestination = string.Format(
                $"{UserImageDestination}_optimized.jpg", 
                this.User.Identity.Name);

            await using var file = FileSystem.OpenRead(userImageDestination);

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

            await ImageService.UpdateImage(pictureUrl, userImageDestination);

            return this.Ok();
        }
    }
}
