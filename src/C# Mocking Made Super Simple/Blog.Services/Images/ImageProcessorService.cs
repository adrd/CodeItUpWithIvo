namespace Blog.Services.Images
{
    using System.Threading.Tasks;
    using Machine;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;

    public class ImageProcessorService : IImageProcessorService
    {
        private readonly IFileSystemService fileSystemService;

        public ImageProcessorService(IFileSystemService fileSystemService) 
            => this.fileSystemService = fileSystemService;

        public (int width, int height) GetSizes(string destination)
        {
            using var image = Image.Load(destination);

            return (image.Width, image.Height);
        }

        public async Task Resize(string source, string destination, int width, int height)
        {
            using var image = Image.Load(source);

            image.Mutate(i => i.Resize(width, height));

            await using var output = this.fileSystemService.OpenWrite(destination);

            image.SaveAsJpeg(output);
        }
    }
}
