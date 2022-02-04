namespace WebServerImages.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Images;
    using Services;

    public class FileImagesController : Controller
    {
        private readonly IFileImageService imageService;

        public FileImagesController(IFileImageService imageService)
            => this.imageService = imageService;

        [HttpGet]
        public IActionResult Upload() => this.View();

        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile[] images)
        {
            if (images.Length > 10)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 10 images!");
                return this.View();
            }

            await this.imageService.Process(images.Select(i => new ImageInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }));

            return this.RedirectToAction(nameof(this.Done));
        }

        public async Task<IActionResult> All()
            => this.View(await this.imageService.GetAllImages());

        public IActionResult Done() => this.View();
    }
}
