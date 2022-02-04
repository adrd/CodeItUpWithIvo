namespace MusicStore.Services.ShoppingCart
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class ShoppingCartProvider : IShoppingCartProvider
    {
        private const string ShoppingCartSessionKey = "Session";

        private readonly HttpContext _httpContext;

        public ShoppingCartProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetCurrentCart()
        {
            var cartId = _httpContext.Session.GetString(ShoppingCartSessionKey);

            if (cartId == null)
            {
                cartId = Guid.NewGuid().ToString();

                _httpContext.Session.SetString(ShoppingCartSessionKey, cartId);
            }

            return cartId;
        }
    }
}
