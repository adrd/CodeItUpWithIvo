namespace WebServerImages.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Images;

    public interface IFileImageService
    {
        Task Process(IEnumerable<ImageInputModel> images);

        Task<List<string>> GetAllImages();
    }
}
