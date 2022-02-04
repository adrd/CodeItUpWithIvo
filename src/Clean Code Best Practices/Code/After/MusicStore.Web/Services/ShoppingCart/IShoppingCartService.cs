namespace MusicStore.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IShoppingCartService
    {
        Task<List<CartItem>> GetCartItems();

        Task<decimal> GetTotal();

        Task AddToCart(int albumId);
    }
}
