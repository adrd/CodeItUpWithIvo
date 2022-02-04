namespace Blog.Services.Web
{
    using System.Threading.Tasks;

    public interface IWebClientService
    {
        Task DownloadFile(string address, string fileName);
    }
}
