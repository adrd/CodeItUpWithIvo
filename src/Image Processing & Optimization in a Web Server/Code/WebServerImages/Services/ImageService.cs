namespace WebServerImages.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Images;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    public class ImageService : IImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullscreenWidth = 1000;

        private readonly ApplicationDbContext data;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public ImageService(
            ApplicationDbContext data,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.data = data;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Process(IEnumerable<ImageInputModel> images)
        {
            // Instead of IServiceScopeFactory, we may use a concurrent dictionary to get the image data and then store everything in the database with a single transaction.

            var tasks = images
                .Select(image => Task.Run(async () =>
                {
                    try
                    {
                        using var imageResult = await Image.LoadAsync(image.Content);

                        var original = await this.SaveImage(imageResult, imageResult.Width);
                        var fullscreen = await this.SaveImage(imageResult, FullscreenWidth);
                        var thumbnail = await this.SaveImage(imageResult, ThumbnailWidth);

                        var database = this.serviceScopeFactory
                            .CreateScope()
                            .ServiceProvider
                            .GetRequiredService<ApplicationDbContext>();

                        database.ImageData.Add(new ImageData
                        {
                            OriginalFileName = image.Name,
                            OriginalType = image.Type,
                            OriginalContent = original,
                            ThumbnailContent = thumbnail,
                            FullscreenContent = fullscreen
                        });

                        await database.SaveChangesAsync();
                    }
                    catch
                    {
                        // Log information.
                    }
                }))
                .ToList();

            await Task.WhenAll(tasks);
        }

        public Task<Stream> GetThumbnail(string id)
            => this.GetImageData(id, "Thumbnail");

        public Task<Stream> GetFullscreen(string id)
            => this.GetImageData(id, "Fullscreen");

        public Task<List<string>> GetAllImages()
            => this.data
                .ImageData
                .Select(i => i.Id.ToString())
                .ToListAsync();

        private async Task<byte[]> SaveImage(Image image, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image
                .Mutate(i => i
                    .Resize(new Size(width, height)));

            image.Metadata.ExifProfile = null;

            var memoryStream = new MemoryStream();

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
            {
                Quality = 75
            });

            return memoryStream.ToArray();
        }

        private async Task<Stream> GetImageData(string id, string size)
        {
            var database = this.data.Database;

            var dbConnection = (SqlConnection)database.GetDbConnection();

            var command = new SqlCommand(
                $"SELECT {size}Content FROM ImageData WHERE Id = @id;",
                dbConnection);

            command.Parameters.Add(new SqlParameter("@id", id));

            dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();

            Stream result = null;

            if (reader.HasRows)
            {
                while (reader.Read()) result = reader.GetStream(0);
            }

            reader.Close();

            return result;
        }
    }
}
