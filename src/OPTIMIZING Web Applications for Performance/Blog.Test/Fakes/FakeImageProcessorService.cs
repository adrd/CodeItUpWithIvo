namespace Blog.Test.Fakes
{
    using System.Threading.Tasks;
    using Blog.Services.Images;

    public class FakeImageProcessorService : IImageProcessorService
    {
        private const int FakeImageSize = 1000;

        public bool ImageResized { get; private set; }

        public string ImageSource { get; private set; }

        public string ImageDestination { get; private set; }

        public (int width, int height) GetSizes(string destination)
            => (FakeImageSize, FakeImageSize);

        public Task Resize(string source, string destination, int width, int height)
        {
            this.ImageResized = true;
            this.ImageSource = source;
            this.ImageDestination = destination;

            return Task.CompletedTask;
        }
    }
}
