namespace Blog.Tests.Fakes
{
    using System.Collections.Generic;
    using Blog.Services;
    using Blog.Services.Models;
    using Moq;

    public class ArticleServiceFake
    {
        public int PageSize { get; private set; }

        public IArticleService Instance
        {
            get
            {
                var articleService = new Mock<IArticleService>();

                articleService
                    .Setup(a => a.All(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<bool>()))
                    .ReturnsAsync((int page, int pageSize, bool publicOnly) =>
                    {
                        this.PageSize = pageSize;

                        return new List<ArticleListingServiceModel>
                        {
                            new ArticleListingServiceModel()
                        };
                    });

                return articleService.Object;
            }
        }
    }
}
