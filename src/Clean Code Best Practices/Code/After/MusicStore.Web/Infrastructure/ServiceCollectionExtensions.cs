namespace MusicStore.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Services.Albums;
    using Services.Genres;
    using Services.ShoppingCart;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
            => services
                .AddTransient<IGenreService, GenreService>()
                .AddTransient<IAlbumService, AlbumService>()
                .AddTransient<IShoppingCartProvider, ShoppingCartProvider>()
                .AddTransient<IShoppingCartService, ShoppingCartService>();
    }
}
