namespace MusicStore.Services.Albums
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AlbumService : IAlbumService
    {
        private readonly MusicStoreContext _dbContext;

        public AlbumService(MusicStoreContext dbContext)
            => this._dbContext = dbContext;

        public async Task<bool> Exists(int albumId)
            => await _dbContext.Albums.AnyAsync(a => a.AlbumId == albumId);
    }
}
