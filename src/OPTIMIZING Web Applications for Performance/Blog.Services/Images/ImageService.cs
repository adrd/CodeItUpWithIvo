namespace Blog.Services.Images
{
    using System.Threading.Tasks;
    using Web;

    public class ImageService : IImageService
    {
        private readonly IWebClientService webClientService;
        private readonly IImageProcessorService imageProcessorService;

        public ImageService(
            IWebClientService webClientService,
            IImageProcessorService imageProcessorService)
        {
            this.webClientService = webClientService;
            this.imageProcessorService = imageProcessorService;
        }

        public async Task UpdateImage(
            string imageUrl, 
            string destination, 
            int? width = null,
            int? height = null)
        {
            var destinationPath = $"{destination}.jpg";

            await this.webClientService.DownloadFile(imageUrl, destinationPath);

            var (imageWidth, imageHeight) = this.imageProcessorService.GetSizes(destinationPath);

            var (optimalWidth, optimalHeight) = this.CalculateOptimalSize(
                width,
                height,
                imageWidth,
                imageHeight);

            var newDestination = $"{destination}_optimized.jpg";

            await this.imageProcessorService.Resize(destinationPath, newDestination, optimalWidth, optimalHeight);
        }

        // Internal for testing purposes.
        internal (int width, int height) CalculateOptimalSize(
            int? width,
            int? height,
            int originalWidth,
            int originalHeight)
        {
            const int minimumSize = 100;

            width ??= originalWidth;
            height ??= originalHeight;

            if (width < minimumSize)
            {
                width = minimumSize;
            }

            if (height < minimumSize)
            {
                height = minimumSize;
            }

            if (width > originalWidth)
            {
                width = originalWidth;
            }

            if (height > originalHeight)
            {
                height = originalHeight;
            }

            return (width.Value, height.Value);
        }
    }
}
