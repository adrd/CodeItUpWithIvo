namespace Blog.Services
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task UpdateImage(
            string imageUrl,
            string destination,
            int? width = null,
            int? height = null);
    }
}
