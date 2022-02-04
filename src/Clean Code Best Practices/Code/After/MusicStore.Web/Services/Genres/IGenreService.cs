namespace MusicStore.Services.Genres
{
    using System.Threading.Tasks;
    using Models;

    public interface IGenreService
    {
        Task<Genre> FindByName(string genre, bool includeAlbums = false);
    }
}
