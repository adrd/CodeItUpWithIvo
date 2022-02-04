using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Models;
using MusicStore.ViewModels;
using MusicStore.Services.Albums;
using MusicStore.Services.ShoppingCart;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly MusicStoreContext _dbContext;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(
            IAlbumService albumService,
            IShoppingCartService shoppingCartService,
            MusicStoreContext dbContext, 
            ILogger<ShoppingCartController> logger)
        {
            _albumService = albumService;
            _shoppingCartService = shoppingCartService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = await _shoppingCartService.GetCartItems(),
                CartTotal = await _shoppingCartService.GetTotal()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var albumExists = await _albumService.Exists(id);

            if (!albumExists)
            {
                return NotFound();
            }

            await _shoppingCartService.AddToCart(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(
            int id,
            CancellationToken requestAborted)
        {
            // Retrieve the current user's shopping cart
            var cart = ShoppingCart.GetCart(this._dbContext, HttpContext);

            // Get the name of the album to display confirmation
            var cartItem = await this._dbContext.CartItems
                .Where(item => item.CartItemId == id)
                .Include(c => c.Album)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;
            if (cartItem != null)
            {
                // Remove from cart
                itemCount = cart.RemoveFromCart(id);

                await this._dbContext.SaveChangesAsync(requestAborted);

                string removed = (itemCount > 0) ? " 1 copy of " : string.Empty;
                message = removed + cartItem.Album.Title + " has been removed from your shopping cart.";
            }
            else
            {
                itemCount = 0;
                message = "Could not find this item, nothing has been removed from your shopping cart.";
            }

            // Display the confirmation message

            var results = new ShoppingCartRemoveViewModel
            {
                Message = message,
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            _logger.LogInformation("Album {id} was removed from a cart.", id);

            return Json(results);
        }
    }
}