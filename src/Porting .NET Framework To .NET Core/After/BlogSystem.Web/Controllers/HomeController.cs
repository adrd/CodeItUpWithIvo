namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using AutoMapper;

    using BlogSystem.Data.Contracts;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private const int PostsPerPageDefaultValue = 5;

        private readonly IRepository<BlogPost> blogPostsData;
        private readonly IMapper mapper;

        public HomeController(
            IRepository<BlogPost> blogPosts, 
            IMapper mapper)
        {
            this.blogPostsData = blogPosts;
            this.mapper = mapper;
        }

        public ActionResult Index(int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.blogPostsData.All().Count() / (decimal)perPage);

            var posts = this.mapper
                .ProjectTo<BlogPostAnnotationViewModel>(this.blogPostsData
                    .All()
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.CreatedOn))
                .ToList()
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var model = new IndexViewModel
            {
                Posts = posts.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        /// <summary>
        /// Gets the robots.txt file.
        /// </summary>
        /// <returns>Returns a robots.txt file.</returns>
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public FileResult RobotsTxt()
        {
            var robotsTxtContent = new StringBuilder();
            robotsTxtContent.AppendLine("User-Agent: *");
            robotsTxtContent.AppendLine("Allow: /");

            return this.File(Encoding.ASCII.GetBytes(robotsTxtContent.ToString()), "text/plain");
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}