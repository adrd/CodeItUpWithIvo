namespace Blog.Tests.Services
{
    using Blog.Services;
    using Shouldly;
    using Xunit;

    public class ImageServiceTests
    {
        [Fact]
        public void CalculateOptimalSize_ShouldMinimumSize_WhenWidthIsLessThanIt()
        {
            // Arrange
            var imageService = new ImageService();

            // Act
            var (width, _) = imageService.CalculateOptimalSize(50, 50, 250, 250);

            // Assert
            width.ShouldBe(100);
        }
    }
}
