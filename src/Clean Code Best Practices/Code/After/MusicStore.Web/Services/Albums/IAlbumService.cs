namespace MusicStore.Services.Albums
{
    using System.Threading.Tasks;

    public interface IAlbumService
    {
        Task<bool> Exists(int albumId);
    }
}
