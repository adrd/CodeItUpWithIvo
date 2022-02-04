namespace Blog.Test.Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Blog.Services;
    using Blog.Services.Data;
    using Blog.Services.Infrastructure;
    using Common;
    using Fakes;
    using Xunit;

    public class ArticleServiceTest : TestWithData
    {
        [Fact]
        public async Task IsByUserShouldReturnTrueWhenArticleByTheSpecificUserExists()
        {
            // Arrange
            var articleService = await this.GetArticleService("ArticlesIsByUserExists");

            // Act
            var exists = await articleService.IsByUser(1, "1");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task IsByUserShouldReturnFalseWhenArticleByTheSpecificUserDoesNotExist()
        {
            // Arrange
            var articleService = await this.GetArticleService("ArticlesIsByUserDoesNotExist");

            // Act
            var exists = await articleService.IsByUser(3, "1");

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task AllShouldReturnCorrectArticlesWithDefaultParameters()
        {
            // Arrange
            var articleService = await this.GetArticleService("AllArticlesWithDefaultParameters");

            // Act
            var articles = await articleService.All();

            // Assert
            var article = Assert.Single(articles);
            Assert.NotNull(article);
            Assert.Equal(2, article.Id);
        }

        [Fact]
        public async Task ChangeVisibilityShouldSetCorrectPublishedOnDate()
        {
            // Arrange
            const int articleId = 1;
            var articleService = await GetArticleService("ChangeVisibility");

            // Act
            await articleService.ChangeVisibility(articleId);

            // Assert
            var article = this.Database.Articles.Find(articleId);

            Assert.NotNull(article);
            Assert.True(article.IsPublic);
            Assert.Equal(new DateTime(2020, 1, 1), article.PublishedOn);
        }

        private async Task<ArticleService> GetArticleService(string databaseName)
        {
            await this.InitializeDatabase(databaseName);

            var mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.AddProfile<ServiceMappingProfile>();
            }));

            return new ArticleService(this.Database, mapper, new FakeDateTimeService());
        }
    }
}
