namespace MusicStore.Services.Genres
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class GenreService : IGenreService
    {
        private readonly MusicStoreContext _dbContext;

        public GenreService(MusicStoreContext dbContext) 
            => this._dbContext = dbContext;

        public async Task<Genre> FindByName(string genre, bool includeAlbums = false)
        {
            var query = this._dbContext.Genres
                .Where(g => g.Name == genre);

            if (includeAlbums)
            {
                query = query.Include(g => g.Albums);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
