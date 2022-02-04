namespace Blog.Tests.Services
{
    using System.Threading.Tasks;
    using Blog.Services;
    using Data.Models;
    using Infrastructure;
    using Shouldly;
    using Xunit;

    public class ArticleServiceTests : TestsWithData
    {
        private const int ArticleId = 1;
        private const string UserId = "1";

        [Fact]
        public async Task IsByUser_ShouldReturnTrue_IfArticleExists()
        {
            // Arrange
            var articleService = new ArticleService(this.Data);
            this.AddArticles();

            // Act
            var result = await articleService.IsByUser(ArticleId, UserId);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task IsByUser_ShouldReturnFalse_IfArticleDoesNotExist()
        {
            // Arrange
            var articleService = new ArticleService(this.Data);
            this.AddArticles();

            // Act
            var result = await articleService.IsByUser(ArticleId, "2");

            // Assert
            result.ShouldBeFalse();
        }

        private void AddArticles()
        {
            this.AddData(new Article
            {
                Id = ArticleId,
                UserId = UserId
            });
        }
    }
}
