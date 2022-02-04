namespace MusicStore.Services.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ShoppingCartService : IShoppingCartService

    {
        private readonly string _shoppingCart;
        private readonly MusicStoreContext _dbContext;

        public ShoppingCartService(
            IShoppingCartProvider shoppingCartProvider,
            MusicStoreContext dbContext)
        {
            _shoppingCart = shoppingCartProvider.GetCurrentCart();
            _dbContext = dbContext;
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return _dbContext
                .CartItems
                .Where(cart => cart.CartId == _shoppingCart)
                .Include(c => c.Album)
                .ToListAsync();
        }

        public Task<decimal> GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            return _dbContext
                .CartItems
                .Include(c => c.Album)
                .Where(c => c.CartId == _shoppingCart)
                .Select(c => c.Album.Price * c.Count)
                .SumAsync();
        }

        public async Task AddToCart(int albumId)
        {
            var cartItem = await _dbContext
                .CartItems
                .SingleOrDefaultAsync(
                    c => c.CartId == _shoppingCart
                     && c.AlbumId == albumId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    AlbumId = albumId,
                    CartId = _shoppingCart,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
