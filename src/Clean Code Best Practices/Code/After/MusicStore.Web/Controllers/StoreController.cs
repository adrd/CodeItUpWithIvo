using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MusicStore.Models;
using MusicStore.Infrastructure;
using MusicStore.Services.Genres;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly MusicStoreContext _dbContext;
        private readonly AppSettings _appSettings;

        public StoreController(
            IGenreService genreService,
            MusicStoreContext dbContext, 
            IOptions<AppSettings> options)
        {
            _genreService = genreService;
            _dbContext = dbContext;
            _appSettings = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _dbContext.Genres.ToListAsync();

            return View(genres);
        }

        public async Task<IActionResult> Browse(string genre)
        {
            var genreModel = await _genreService.FindByName(genre, true);

            return this.ViewOrNotFound(genreModel);
        }

        public async Task<IActionResult> Details(
            [FromServices] IMemoryCache cache,
            int id)
        {
            var cacheKey = $"album_{id}";
            if (!cache.TryGetValue(cacheKey, out Album album))
            {
                album = await _dbContext.Albums
                    .Where(a => a.AlbumId == id)
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .FirstOrDefaultAsync();

                if (album != null)
                {
                    if (_appSettings.CacheDbResults)
                    {
                        var cacheOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                        cache.Set(
                            cacheKey,
                            album,
                            cacheOptions);
                    }
                }
            }

            return this.ViewOrNotFound(album);
        }
    }
}