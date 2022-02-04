namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController()
            => this.articleService = new ArticleService();

        public async Task<IActionResult> Index()
        {
            var articles = await this.articleService.All(pageSize: 3);

            return this.View(articles);
        }

        public IActionResult About() => this.View();

        [Authorize]
        public IActionResult Privacy() => this.View(new PrivacyViewModel
        {
            Username = this.User.Identity.Name
        });
    }
}
