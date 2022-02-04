namespace Blog.Services.Web
{
    using System.Net;
    using System.Threading.Tasks;

    public class WebClientService : IWebClientService
    {
        private readonly WebClient webClient;

        public WebClientService()
            => this.webClient = new WebClient();

        public async Task DownloadFile(string address, string fileName) 
            => await this.webClient.DownloadFileTaskAsync(address, fileName);
    }
}
