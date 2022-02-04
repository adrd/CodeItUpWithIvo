namespace Blog.Services
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;

    public static class ImageService
    {
        public static async Task UpdateImage(
            string imageUrl, 
            string destination, 
            int? width = null,
            int? height = null)
        {
            var webClient = new WebClient();

            destination = $"{destination}.jpg";

            await webClient.DownloadFileTaskAsync(imageUrl, destination);

            using var image = Image.Load(destination);

            (int optimalWidth, int optimalHeight) = CalculateOptimalSize(
                width, 
                height, 
                image.Width, 
                image.Height);

            image.Mutate(i => i.Resize(optimalWidth, optimalHeight));

            await using var output = File.OpenWrite($"{destination}_optimized.jpg");

            image.SaveAsJpeg(output);
        }

        private static (int width, int height) CalculateOptimalSize(
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
