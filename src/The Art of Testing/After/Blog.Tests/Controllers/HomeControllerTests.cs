namespace Blog.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Blog.Controllers;
    using Blog.Controllers.Models;
    using Blog.Services.Models;
    using Fakes;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using static Blog.Controllers.ControllerConstants;

    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_ShouldReturnView_WithCorrectArticles()
        {
            // Arrange
            var articleServiceFake = new ArticleServiceFake();
            var homeController = new HomeController(articleServiceFake.Instance);

            // Act
            var result = await homeController.Index();

            // Assert
            articleServiceFake.PageSize.ShouldBe(HomeIndexArticlePageSize);

            result
                .ShouldBeAssignableTo<ViewResult>()
                .Model
                .ShouldBeAssignableTo<IEnumerable<ArticleListingServiceModel>>();
        }

        [Fact]
        public void About_ShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act
            var result = homeController.About();

            // Assert
            result.ShouldBeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Private_ShouldReturnView_WithModel()
        {
            // Arrange
            var homeController = new HomeController(null)
                .WithTestUser();

            // Act
            var result = homeController.Privacy();

            // Assert
            result
                .ShouldBeAssignableTo<ViewResult>()
                .Model
                .ShouldBeAssignableTo<PrivacyViewModel>()
                .Username
                .ShouldBe(TestConstants.TestUsername);
        }
    }
}
