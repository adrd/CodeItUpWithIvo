namespace WebServerImages.Services
{
    using System;
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

    public class FileImageService : IFileImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullscreenWidth = 1000;

        private readonly ApplicationDbContext data;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public FileImageService(
            ApplicationDbContext data,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.data = data;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Process(IEnumerable<ImageInputModel> images)
        {
            // Instead of IServiceScopeFactory, we may use a concurrent dictionary to get the image data and then store everything in the database with a single transaction.

            var totalImages = await this
                .data
                .ImageFiles
                .CountAsync();

            var tasks = images
                .Select(image => Task.Run(async () =>
                {
                    try
                    {
                        using var imageResult = await Image.LoadAsync(image.Content);

                        var id = Guid.NewGuid();
                        var path = $"/images/{totalImages % 1000}/";
                        var name = $"{id}.jpg";

                        var storagePath = Path.Combine(
                            Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                        if (!Directory.Exists(storagePath))
                        {
                            Directory.CreateDirectory(storagePath);
                        }

                        await this.SaveImage(imageResult,
                            $"Original_{name}", storagePath, imageResult.Width);
                        await this.SaveImage(imageResult,
                            $"Fullscreen_{name}", storagePath, FullscreenWidth);
                        await this.SaveImage(imageResult,
                            $"Thumbnail_{name}", storagePath, ThumbnailWidth);

                        var database = this.serviceScopeFactory
                            .CreateScope()
                            .ServiceProvider
                            .GetRequiredService<ApplicationDbContext>();

                        database.ImageFiles.Add(new ImageFile
                        {
                            Id = id,
                            Folder = path
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

        public Task<List<string>> GetAllImages()
            => this.data
                .ImageFiles
                .Select(i => i.Folder + "/Thumbnail_" + i.Id + ".jpg")
                .ToListAsync();

        private async Task SaveImage(Image image, string name, string path, int resizeWidth)
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

            await image.SaveAsJpegAsync($"{path}/{name}", new JpegEncoder
            {
                Quality = 75
            });
        }
    }
}
