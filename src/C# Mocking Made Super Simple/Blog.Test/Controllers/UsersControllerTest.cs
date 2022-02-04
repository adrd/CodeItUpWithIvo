namespace Blog.Test.Controllers
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Blog.Controllers;
    using Blog.Services.Images;
    using Extensions;
    using FakeItEasy;
    using Fakes;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    using static Blog.Controllers.ControllerConstants;

    public class UsersControllerTest
    {
        [Fact]
        public async Task ChangeProfilePictureWithNullPictureUrlShouldReturnBadRequest()
        {
            // Arrange
            var usersController = new UsersController(null, null);

            // Act
            var result = await usersController.ChangeProfilePicture(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Image cannot be empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task ChangeProfilePictureWithNonNullPictureUrlShouldReturnOk()
        {
            // Arrange
            const string pictureUrl = "TestPictureUrl";

            var fakeImageService = A.Fake<IImageService>();

            string fakeImageUrl = null;
            string fakeDestination = null;

            A
                .CallTo(() => fakeImageService.UpdateImage(
                    A<string>.Ignored,
                    A<string>.Ignored,
                    A<int?>.Ignored,
                    A<int?>.Ignored))
                .Invokes((string imageUrl,
                    string destination,
                    int? width,
                    int? height) =>
                {
                    fakeImageUrl = imageUrl;
                    fakeDestination = destination;
                });

            var usersController = new UsersController(fakeImageService, null)
                .WithTestUser();
            
            // Act
            var result = await usersController.ChangeProfilePicture(pictureUrl);

            // Assert
            Assert.Equal(pictureUrl, fakeImageUrl);
            Assert.Equal(@$"Images\Users\{TestConstants.TestUsername}", fakeDestination);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetProfilePictureShouldReturnCorrectFileStreamResult()
        {
            // Arrange
            var usersController = new UsersController(null, new FakeFileSystemService())
                .WithTestUser();

            // Act
            var result = await usersController.GetProfilePicture();

            // Assert
            var fileStreamResult = Assert.IsType<FileStreamResult>(result);
            var memoryStream = Assert.IsType<MemoryStream>(fileStreamResult.FileStream);

            var memoryStreamData = Encoding.UTF8.GetString(memoryStream.ToArray());
            var expectedProfilePicturePath = string.Format(
                $"{UserImageDestination}_optimized.jpg",
                TestConstants.TestUsername);

            Assert.Equal(expectedProfilePicturePath, memoryStreamData);
            Assert.Equal(ImageContentType, fileStreamResult.ContentType);
        }
    }
}
