namespace Blog.Test.Fakes
{
    using System.Threading.Tasks;
    using Blog.Services.Web;

    public class FakeWebClientService : IWebClientService
    {
        public bool FileDownloaded { get; private set; }

        public string DownloadDestination { get; private set; }

        public Task DownloadFile(string address, string fileName)
        {
            this.FileDownloaded = true;
            this.DownloadDestination = fileName;

            return Task.CompletedTask;
        }
    }
}
