namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Data;

    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
            => this.articleService = articleService;

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
