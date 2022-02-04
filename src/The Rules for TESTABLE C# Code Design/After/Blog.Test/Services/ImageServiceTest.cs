namespace Blog.Test.Services
{
    using System.Threading.Tasks;
    using Blog.Services.Images;
    using Fakes;
    using Xunit;

    public class ImageServiceTest
    {
        [Fact]
        public void CalculateOptimalSizeShouldReturnMinimumSizeWhenSizeIsLessThanTheAllowedMinimum()
        {
            // Arrange
            const int minimumSize = 100;
            const int originalSize = 200;
            const int resizeSize = 50;

            var imageService = new ImageService(null, null);

            // Act
            var (width, height) = imageService
                .CalculateOptimalSize(resizeSize, resizeSize, originalSize, originalSize);

            // Assert
            Assert.Equal(minimumSize, width);
            Assert.Equal(minimumSize, height);
        }

        [Fact]
        public async Task UpdateImageShouldDownloadImageAndResizeItToCorrectDestination()
        {
            // Arrange
            const string imageUrl = "TestImageUrl";
            const string destination = "TestDestination";
            const int size = 200;

            var webClientService = new FakeWebClientService();
            var imageProcessorService = new FakeImageProcessorService();
            var imageService = new ImageService(webClientService, imageProcessorService);

            // Act
            await imageService.UpdateImage(imageUrl, destination, size, size);

            // Assert
            var imageDestination = $"{destination}.jpg";

            Assert.True(webClientService.FileDownloaded);
            Assert.Equal($"{destination}.jpg", webClientService.DownloadDestination);
            Assert.True(imageProcessorService.ImageResized);
            Assert.Equal(imageDestination, imageProcessorService.ImageSource);
            Assert.Equal($"{destination}_optimized.jpg", imageProcessorService.ImageDestination);
        }
    }
}
