namespace WebServerImages.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Models.Images;

    public interface IImageService
    {
        Task Process(IEnumerable<ImageInputModel> images);

        Task<Stream> GetThumbnail(string id);

        Task<Stream> GetFullscreen(string id);

        Task<List<string>> GetAllImages();
    }
}
